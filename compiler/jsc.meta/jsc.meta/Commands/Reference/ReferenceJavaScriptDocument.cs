﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using jsc.Languages.IL;
using jsc.Library;
using jsc.meta.Commands.Reference.ReferenceUltraSource.Plugins;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Library;
using jsc.meta.Library.Templates.JavaScript;
using jsc.meta.Tools;
using jsc.Script;
using Microsoft.CSharp;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Lookup;
using System.Collections;
using System.Collections.Specialized;

namespace jsc.meta.Commands.Reference
{
    [Description("Injecting javascript into HTML has never been that easy!")]
    public partial class ReferenceJavaScriptDocument : ReferenceUltraSource.ReferenceUltraSource
    {


        public override void Invoke()
        {
            if (this.AttachDebugger)
                Debugger.Launch();

            var csproj = XDocument.Load(ProjectFileName.FullName);
            var csproj_dirty = false;



            /*

<Project ToolsVersion="3.5" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <RootNamespace>AutoGeneratedReferences</RootNamespace>

  <ItemGroup>
    <Reference Include="System" />

  <ItemGroup>
    <None Include="Components\JohDoe.TextComponent" />
            */

            XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
            var nsItemGroup = ns + "ItemGroup";
            var nsRootNamespace = ns + "RootNamespace";
            var nsPropertyGroup = ns + "PropertyGroup";
            var nsNone = ns + "None";
            var nsContent = ns + "Content";
            var nsPage = ns + "Page";
            var nsLink = ns + "Link";
            var nsDependentUpon = ns + "DependentUpon";
            var nsReference = ns + "Reference";
            var nsHintPath = ns + "HintPath";
            var nsAssemblyName = ns + "AssemblyName";
            //var nsInclude = ns + "Include";

            var SourceAssemblyName = Enumerable.First(
                 from PropertyGroup in csproj.Root.Elements(nsPropertyGroup)
                 from AssemblyName in PropertyGroup.Elements(nsAssemblyName)
                 select AssemblyName.Value
            );

            var DefaultNamespace = Enumerable.First(
                 from PropertyGroup in csproj.Root.Elements(nsPropertyGroup)
                 //from RootNamespace in PropertyGroup.Elements(nsRootNamespace)
                 //select RootNamespace.Value

                 from __AssemblyName in PropertyGroup.Elements(nsAssemblyName)
                 select __AssemblyName.Value
            );

            // bin is assumed to being ignored by svn
            // we need to stage it
            var Staging = this.ProjectFileName.Directory.CreateSubdirectory("bin/staging." + UltraSource);

            // fixme: no caching as of yet
            //var Cache = Staging.CreateSubdirectory("cache");

            Func<FileInfo, bool> HasReference =
                AssemblyFile =>
                {
                    var TargetHintPath = AssemblyFile.FullName.Substring(ProjectFileName.Directory.FullName.Length + 1);

                    return Enumerable.Any(
                         from ItemGroup in csproj.Root.Elements(nsItemGroup)
                         from Reference in ItemGroup.Elements(nsReference)
                         from HintPath in Reference.Elements(nsHintPath)
                         where TargetHintPath == HintPath.Value
                         select new { HintPath, Reference, ItemGroup }
                    );
                };

            #region AddReference
            Action<FileInfo, AssemblyName> AddReference =
                (AssemblyFile, Name) =>
                {

                    /* add reference
<Reference Include="AutoGeneratedReferences.Components.JohDoe.TextComponent, Version=0.0.0.0, Culture=neutral, processorArchitecture=MSIL">
  <SpecificVersion>False</SpecificVersion>
  <HintPath>bin\staging\AutoGeneratedReferences.Components.JohDoe.TextComponent.dll</HintPath>
</Reference>
                    */

                    var TargetHintPath = AssemblyFile.FullName.Substring(ProjectFileName.Directory.FullName.Length + 1);

                    if (!HasReference(AssemblyFile))
                    {
                        var TargetItemGroup = Enumerable.First(
                            from ItemGroup in csproj.Root.Elements(nsItemGroup)
                            from Reference in ItemGroup.Elements(nsReference)
                            select ItemGroup
                        );

                        TargetItemGroup.Add(
                            new XElement(nsReference,
                                new XAttribute("Include", Name.ToString()),
                                new XElement(nsHintPath, TargetHintPath)
                            )
                        );

                        csproj_dirty = true;

                    }
                };
            #endregion


            var Targets =
              from ItemGroup in csproj.Root.Elements(nsItemGroup)

              from None in ItemGroup.Elements(nsNone).Concat(ItemGroup.Elements(nsContent)).Concat(ItemGroup.Elements(nsPage))

              let Link = None.Element(nsLink)

              let Include = None.Attribute("Include").Value

              // Directory In Project
              let Directory = Path.GetDirectoryName(Link != null ? Link.Value : Include).Replace("\\", "/")

              // The project direcotry is .UltraSource
              where DirectoryNeedsConversion(Directory)

              let TargetName = DefaultNamespace + "." + Directory.Replace("/", ".").Replace("\\", ".")

              let Target = new FileInfo(Path.Combine(Staging.FullName, TargetName.Substring(DefaultNamespace.Length + 1) + ".dll"))

              let File = new FileInfo(Link != null ? Include : Path.Combine(ProjectFileName.Directory.FullName, Include))

              group new
              {
                  ItemGroup,
                  None,
                  Include = Link != null ? Link.Value : Include,
                  File,
                  Directory,
                  TargetName,
                  //Target
              } by Directory;

            // F# lazy?
            #region ReferencedConcepts
            Func<Type[]> ReferencedConcepts = delegate
            {
                // this method shall run once...

                var ReferencedInterfaces =
                    from PropertyGroup in csproj.Root.Elements(nsItemGroup)

                    // what about ProjectReference ?
                    from Reference in PropertyGroup.Elements(nsReference)
                    let Include = Reference.Attribute("Include").Value

                    // we are not loading a previous version of the generated assembly.
                    where !Include.EndsWith("." + UltraSource)

                    let HintPaths = Reference.Elements(nsHintPath).Select(k => new FileInfo(k.Value)).Where(k => k.Exists)


                    let HintPath = HintPaths.Any() ? HintPaths.First() : null

                    let Assembly = HintPath != null ? Assembly.LoadFile(HintPath.FullName) : Assembly.LoadWithPartialName(Include)

                    // why do we get null?
                    where Assembly != null

                    from ExportedType in Assembly.GetExportedTypes()

                    where ExportedType.IsInterface
                    where !ExportedType.IsGenericTypeDefinition

                    let GetValidInterfaces = new Func<IEnumerable<Type>, IEnumerable<Type>>(
                        Interfaces =>
                            from Interface in Interfaces
                            where Interface.IsInterface
                            where !Interface.IsGenericTypeDefinition

                            // what if that interface extends other interfaces?
                            let Properties = Interface.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)

                            where Properties.Any()
                            where Properties.All(k => typeof(IHTMLElement).IsAssignableFrom(k.PropertyType))
                            select Interface
                    )

                    let AllInterfaces = ExportedType.GetInterfaces().Except(new[] { typeof(IUltraComponent) }).Concat(new[] { ExportedType })

                    where AllInterfaces.SequenceEqual(GetValidInterfaces(AllInterfaces))

                    select ExportedType;

                return ReferencedInterfaces.ToArray();
            };
            #endregion



            var LocalSources = Enumerable.ToArray(
                from k in Targets

                let IsRoot = k.Key == ""

                let kWithChildren =
                    from c in Targets

                    where IsRoot || c.Key == k.Key || c.Key.StartsWith(k.Key + "/")
                    select c

                from f in k
                where f.File.Name.EndsWith(".htm")
                select new SourceFile
                {
                    TargetFile = f.File,

                    Content = File.ReadAllText(f.File.FullName),
                    Reference = f.File.FullName,
                    GetLocalResource =
                        n =>
                        {

                            var r = kWithChildren.SelectMany(kk => kk).SingleOrDefault(kk =>

                                IsRoot ?
                                kk.Include.Replace("\\", "/") == n :
                                kk.Include.Replace("\\", "/").EndsWith("/" + n)

                            );

                            if (r == null) return null;

                            return r.File;
                        }
                }
            );

            var Output = new FileInfo(Path.Combine(Staging.FullName, DefaultNamespace + "." + UltraSource + ".dll"));

            if (!(
                !Output.Exists
                || !HasReference(Output)
                || this.Configuration.BuildAlways
                || LocalSources.Any(k => k.TargetFile.LastWriteTimeUtc > Output.LastWriteTimeUtc)
                || Enumerable.Any(
                        from LinkedAsset in this.LinkedAssets
                        let dir = new DirectoryInfo(LinkedAsset.TargetRoot).WhenExists()
                        where dir != null
                        from file in dir.GetFiles("*", SearchOption.AllDirectories)
                        where file.LastWriteTimeUtc > Output.LastWriteTimeUtc
                        select file
                   )
                ))
            {
                //Console.WriteLine("A version of UltraSource assembly already exists and no files seem need an update. Use 'Assets' build configuration to rebuild.");
                Console.WriteLine("Use 'Assets' build configuration to force a rebuild for assets.");
            }
            else
            {

                #region preparation


                var ImplementConcept__ = new ImplementConcept
                {
                    ReferencedConcepts = ReferencedConcepts.ToCachedFunc()
                };

                // Now lets load our referenced assemblies.


                var References = Enumerable.Distinct(
                    from k in Targets
                    from f in k
                    // should we restrict us to single file or allow multiple files to
                    // enable grouping?
                    where
                        f.File.Name == __References
                    from r in File.ReadAllLines(f.File.FullName)
                    where !string.IsNullOrEmpty(r)
                    where !r.StartsWith("#")
                    select r
                );



                // http://support.microsoft.com/kb/304655
                var Sources = DownloadWebSource(References).Concat(LocalSources).ToArray();

                #endregion


                {
                    // at this time we are not actually merging anything...
                    var r = default(RewriteToAssembly);

                    #region RewriteToAssembly
                    r = new RewriteToAssembly
                    {
                        Output = Output,
                        //staging = Staging,
                        //product = Product,

                        #region if we are going to inject code from jsc we need to copy it
                        rename = new RewriteToAssembly.NamespaceRenameInstructions[] {
					    "jsc.meta->" +  DefaultNamespace,
					    "jsc->" +  DefaultNamespace,
					},

                        merge = new RewriteToAssembly.MergeInstruction[] {
					    "jsc.meta",
					    "jsc"
					},
                        #endregion

                        // we do not want to merge ScriptCoreLib.Ultra
                        DisableIsMarkedForMerge = true,

                        PostAssemblyRewrite =
                            a =>
                            {
                                // at this point we are free to add any additional code here
                                // maybe we should infer some cool classes?


                                a.Assembly.DefineAttribute<ObfuscationAttribute>(
                                    new
                                    {
                                        Feature =
                                            this.IsMerge ? "merge" : "script"
                                    }
                                );


                                this.LinkedAssets.WithEach(
                                    LinkedAsset =>
                                    {
                                        /*
  c:\util\jsc\publish\jsc.configuration.application
  c:\util\jsc\publish\publish.htm
  c:\util\jsc\publish\setup.exe
  c:\util\jsc\publish\Application Files\jsc.configuration_1_0_0_6\jsc.configuration.application
  c:\util\jsc\publish\Application Files\jsc.configuration_1_0_0_6\jsc.configuration.exe.deploy
  c:\util\jsc\publish\Application Files\jsc.configuration_1_0_0_6\jsc.configuration.exe.manifest
  c:\util\jsc\publish\Application Files\jsc.configuration_1_0_0_6\jsc.ico.deploy                                         
                                         */

                                        new DirectoryInfo(LinkedAsset.TargetRoot).WhenExists().With(
                                            Directory =>
                                            {
                                                var files = new { AssetPath = default(string), Relative = default(string) }.ToEmptyList();

                                                Directory.GetFiles("*", SearchOption.AllDirectories).WithEach(
                                                  file =>
                                                  {
                                                      var Relative = file.ToRelativePath(Directory);

                                                      Console.WriteLine(Relative);

                                                      var AssetPath = "assets/" + DefaultNamespace + "/" + Relative.Replace(@"\", "/").Replace(" ", "_");

                                                      r.RewriteArguments.ScriptResourceWriter.Add(AssetPath, File.ReadAllBytes(file.FullName));

                                                      files.Add(new { AssetPath, Relative });
                                                  }
                                              );

                                                files.WhenAny().With(
                                                   delegate
                                                   {
                                                       var t = a.Module.DefineType(DefaultNamespace + ".Assets." + Directory.Name.ToCamelCase(), TypeAttributes.Public,
                                                           typeof(StringDictionary)
                                                       );

                                                       var ctor = t.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);

                                                       var il = ctor.GetILGenerator();

                                                       //  public virtual void Add(string key, string value);

                                                       il.Emit(OpCodes.Ldarg_0);
                                                       il.Emit(OpCodes.Call, typeof(StringDictionary).GetConstructor());

                                                       var Add = new Action<StringDictionary>(k => k.Add(null, null)).ToReferencedMethod();

                                                       foreach (var item in files)
                                                       {
                                                           il.Emit(OpCodes.Ldarg_0);
                                                           il.Emit(OpCodes.Ldstr, item.Relative);
                                                           il.Emit(OpCodes.Ldstr, item.AssetPath);
                                                           il.Emit(OpCodes.Call, Add);
                                                       }

                                                       il.Emit(OpCodes.Ret);

                                                       t.CreateType();
                                                   }
                                               );
                                            }
                                        );


                                    }
                                );

                                // Nested types do not play well with type erasure...

                                //var Pages = a.Module.DefineType(DefaultNamespace + ".Pages", TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.Public);

                                // http://www.w3schools.com/tags/ref_entities.asp
                                // http://www.w3schools.com/HTML/html_entities.asp
                                // http://stackoverflow.com/questions/281682/reference-to-undeclared-entity-exception-while-working-with-xml


                                var TypeVariations = new Dictionary<string, TypeVariations>();
                                var RemotingTypeVariations = new Dictionary<string, TypeVariations>();


                                foreach (var item in Sources)
                                {
                                    // http://stackoverflow.com/questions/1039476/reference-to-undeclared-entity-nbsp-why
                                    // http://forums.asp.net/t/1219076.aspx

                                    // dirty fix..
                                    var content = item.Content;

                                    // http://blogs.pingpoet.com/overflow/archive/2005/07/20/6607.aspx
                                    // http://msdn.microsoft.com/en-us/library/bb356942.aspx
                                    // fixme: XmlReader + DTD

                                    // yet another fix
                                    const string doctype_ok = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">";
                                    const string doctype_vs = @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">";

                                    if (content.StartsWith(doctype_vs))
                                        content = doctype_ok + content.Substring(doctype_vs.Length);

                                    content = HTMLEntitiesLookup.Lookup.Aggregate(content,
                                             (i, k) => i.Replace(k.Key, k.Value)
                                     );


                                    // really dirty fix...

                                    // wordpress, why are you making the day harder than it needs to be? :)
                                    content = content.Replace(" xmlns=\"http://www.w3.org/1999/xhtml\"", " ");
                                    content = content.Replace(" xmlns=\"http://www.google.com/ns/jotspot\" ", " ");

                                    // should we use html tidy?
                                    // is the google sites printing view public?
                                    // google sites, why aren't attributes qouted? :|
                                    content = content.Replace(" cellpadding=1 cellspacing=1>", " cellpadding='1' cellspacing='1'>");
                                    content = content.Replace(" target=_top>", " target='_top'>");

                                    // http://stackoverflow.com/questions/66837/when-is-a-cdata-section-necessary-within-a-script-tag
                                    content = content.Replace("<script type=\"text/javascript\">", "<script type=\"text/javascript\"><![CDATA[");
                                    content = content.Replace("<script type=\"text/javascript\" charset=\"utf-8\">", "<script type=\"text/javascript\" charset=\"utf-8\"><![CDATA[");


                                    content = content.Replace("<meta http-equiv=\"X-UA-Compatible\" content=\"chrome=1\">", "");
                                    content = content.Replace("/* <![CDATA[ */", "");
                                    content = content.Replace("/* ]]> */", "");

                                    content = content.Replace("<script>", "<script><![CDATA[");
                                    content = content.Replace("</script>", "]]></script>");
                                    content = content.Replace(">]]></script>", "></script>");

                                    //var reader = XmlReader.Create(new StringReader(content), new XmlReaderSettings { ProhibitDtd = false });
                                    //var xml = XDocument.Load(reader);
                                    ////var nameTable = reader.NameTable;
                                    //var namespaceManager = new XmlNamespaceManager(nameTable);
                                    //namespaceManager.AddNamespace("", "http://www.w3.org/1999/xhtml");

                                    var xml = XDocument.Parse(content);
                                    // http://stackoverflow.com/questions/477962/how-to-create-xelement-with-default-namespace-for-children-without-using-xnamespa
                                    // http://www.experts-exchange.com/Programming/Languages/C_Sharp/Q_24536293.html

                                    XNamespace xhtml = "http://www.w3.org/1999/xhtml";

                                    // For body and each class element
                                    var TitleElement = xml.XPathSelectElement("/html/head/title");

                                    Console.WriteLine("page: " + TitleElement.Value);

                                    var BodyElement = xml.XPathSelectElement("/html/body");

                                    var TitleValue = TitleElement == null || string.IsNullOrEmpty(TitleElement.Value) ?
                                        item.Reference.TakeUntilIfAny("?").SkipUntilLastIfAny("/").TakeUntilIfAny(".") :
                                        TitleElement.Value;

                                    // should we make CamelCaseing optional?
                                    var PageName = CompilerBase.GetSafeLiteral(TitleValue, null).ToCamelCase();

                                    // we need to make the title/page name
                                    // C# compatible :)

                                    // The web application could opt in for dynamic CMS updates... RSS ? :) Download HTML on the server and push updates?


                                    new DefineNamedElements
                                    {

                                        DefaultNamespace = DefaultNamespace,
                                        a = a,
                                        BodyElement = BodyElement,
                                        r = r,
                                        GetLocalResource = item.GetLocalResource,

                                        TypeVariations = TypeVariations,
                                        RemotingTypeVariations = RemotingTypeVariations,

                                        PageName = PageName,
                                        ElementTypes = ElementTypes
                                    }.Define();


                                    new DefineDocumentation
                                    {
                                        Context = this,

                                        DefaultNamespace = DefaultNamespace,
                                        BodyElement = BodyElement,
                                        r = r,
                                        GetLocalResource = item.GetLocalResource,

                                    }.Define();

                                    new DefineXDocuments
                                    {

                                        DefaultNamespace = DefaultNamespace,
                                        BodyElement = BodyElement,
                                        r = r,
                                        GetLocalResource = item.GetLocalResource,

                                    }.Define();

                                    new SpriteSheet
                                    {
                                        DefaultNamespace = DefaultNamespace,
                                        BodyElement = BodyElement,
                                        r = r,
                                        GetLocalResource = item.GetLocalResource,

                                    }.Define();

                                    new IDLCompiler
                                    {
                                        DefaultNamespace = DefaultNamespace,
                                        BodyElement = BodyElement,
                                        r = r,
                                        GetLocalResource = item.GetLocalResource,
                                    }.Define();

                                    

                                    var VariationsForPages = new Dictionary<string, Dictionary<string, TypeVariationsTuple>>
								    {
									    {"FromAssets",   TypeVariations.ToDictionary(k => k.Key, k => new TypeVariationsTuple { Type = k.Value.FromAssets, Source = k.Value.FromAssetsSource})},
									    {"FromWeb",  TypeVariations.ToDictionary(k => k.Key, k => new TypeVariationsTuple { Type = k.Value.FromWeb, Source = k.Value.FromWebSource })},
									    {"FromBase64", TypeVariations.ToDictionary(k => k.Key, k => new TypeVariationsTuple { Type = k.Value.FromBase64, Source = k.Value.FromBase64Source })},
								    };


                                    var RemotingVariationsForPages = new Dictionary<string, Dictionary<string, TypeVariationsTuple>>
								    {
									    {"FromAssets",   RemotingTypeVariations.ToDictionary(k => k.Key,k => new TypeVariationsTuple { Type = k.Value.FromAssets, Source = k.Value.FromAssetsSource })},
									    {"FromWeb",  RemotingTypeVariations.ToDictionary(k => k.Key,k => new TypeVariationsTuple { Type =  k.Value.FromWeb, Source = k.Value.FromWebSource })},
									    {"FromBase64", RemotingTypeVariations.ToDictionary(k => k.Key, k =>new TypeVariationsTuple { Type =k.Value.FromBase64, Source = k.Value.FromBase64Source })},
								    };

                                    var IPageLookup = new Dictionary<string, TypeBuilder>();
                                    var Continuation = new List<Action>();
                                    foreach (var CurrentVariationForPage in VariationsForPages)
                                    {
                                        DefinePageType(
                                            DefaultNamespace,
                                            r,
                                            a, content,
                                            BodyElement,
                                            PageName,
                                            CurrentVariationForPage.Key,
                                            CurrentVariationForPage.Value,
                                            RemotingVariationsForPages[CurrentVariationForPage.Key],
                                            ImplementConcept__,
                                            //Concepts,
                                            IPageLookup,
                                            Continuation.Add,
                                            "FromAssets"
                                        );


                                    }

                                    Continuation.Invoke();
                                }


                                //Pages.CreateType();
                            }
                    };

                    r.Invoke();
                    #endregion

                    AddReference(r.Output, new AssemblyName(Path.GetFileNameWithoutExtension(Output.FullName)));
                }
            }

            if (csproj_dirty)
                csproj.Save(this.ProjectFileName.FullName);
        }






        public class Counter
        {
            public int Value;
        }


        static class TemplateHolder
        {
            public static IHTMLElement Initialize(IHTMLElement e)
            {
                return null;
            }


            public static void Implementation()
            {
            }
        }


        public class SourceFile
        {
            public FileInfo TargetFile;

            public string Reference;
            public string Content;

            public Func<string, FileInfo> GetLocalResource;
        }

        private static IEnumerable<SourceFile> DownloadWebSource(IEnumerable<string> References)
        {
            foreach (var Reference in References)
            {
                Console.WriteLine("downloading: " + Reference);

                var c = (HttpWebRequest)HttpWebRequest.Create(Reference);

                // http://code.logos.com/blog/2009/06/using_if-modified-since_in_http_requests.html
                // http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.ifmodifiedsince.aspx
                // http://www.acmebinary.com/blog/archive/2006/09/05/252.aspx

                var r = (HttpWebResponse)c.GetResponse();

                try
                {
                    if (r.StatusCode == HttpStatusCode.OK)
                    {
                        var Content = new StreamReader(r.GetResponseStream()).ReadToEnd();

                        yield return new SourceFile { Content = Content, Reference = Reference };
                    }

                }
                finally
                {
                    r.Close();
                }

            }
        }






    }
}
