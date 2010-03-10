﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using java.applet;
using PromotionWebApplication1.HTML.Pages.FromAssets;
using PromotionWebApplication1.Library;
using PromotionWebApplication1.Services;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
//using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Ultra.Library.Delegates;
using PromotionWebApplication.AvalonLogo;
using ScriptCoreLib.JavaScript.Controls;
//using ScriptCoreLib.Shared.Avalon.Extensions;

namespace PromotionWebApplication1
{

	public delegate string AtInstaller(string e);

	public sealed class UltraApplication
	{
		public class AudioLink
		{
			public IHTMLAudio Audio;

			public AudioLink Prev;
			public AudioLink Next;
		}

		public void Button1_click(IEvent e)
		{

		}

		public UltraApplication(IHTMLElement e)
		{
			var DefaultTitle = "jsc solutions";


			Native.Document.title = DefaultTitle;

			StringActionAction GetTitleFromServer = new UltraWebService().GetTitleFromServer;



			GetTitleFromServer(
				n => Native.Document.title = n
			);

			var MyPagesBackground = new IHTMLDiv
			{

			};

			MyPagesBackground.style.overflow = IStyle.OverflowEnum.hidden;
			MyPagesBackground.style.position = IStyle.PositionEnum.absolute;
			MyPagesBackground.style.width = "100%";
			MyPagesBackground.style.height = "100%";
			MyPagesBackground.AttachToDocument();

			var MyPages = new IHTMLDiv
			{

			};

			MyPages.style.overflow = IStyle.OverflowEnum.auto;
			MyPages.style.position = IStyle.PositionEnum.absolute;
			MyPages.style.width = "100%";
			MyPages.style.height = "100%";
			MyPages.AttachToDocument();

			var MyPagesInternal = new IHTMLDiv();

			MyPagesInternal.style.margin = "4em";
			MyPagesInternal.AttachTo(MyPages);

			// http://www.google.com/support/forum/p/Google+Analytics/thread?tid=486a963e463df665&hl=en
			var gapathname = Native.Document.location.pathname;
			var gasearch = Native.Document.location.search;
			var gahash = Native.Window.escape(Native.Document.location.hash);
			var gapageview = gapathname + gasearch + gahash;

			var hash = Native.Document.location.hash;

			#region logo
			{
				if (Native.Document.location.hash == "#/source")
				{
					new IHTMLElement(IHTMLElement.HTMLElementEnum.h1, "Create your own Ultra Application project template").AttachTo(MyPagesInternal);
					var n = new TextEditor(MyPagesInternal);

					n.Width = 600;
					n.Height = 400;

					//n.InnerHTML = "<p>Create your own <b>Ultra Application</b> Project Template</p>";


					new DefaultPage1().Container.AttachTo(n.Document.body);

					var m1 = new SimpleCodeView();

					m1.Container.AttachTo(MyPagesInternal);
					m1.SelectType.onchange +=
						delegate
						{
							m1.TypeName.innerText = m1.SelectType.value;
						};

					m1.SelectEvent.onchange +=
						delegate
						{
							m1.EventName.innerText = m1.SelectEvent.value;
						};

					var download = new IHTMLButton
					 {
						 //disabled = true
					 };

					download.style.margin = "1em";

					var download_text = new IHTMLSpan("I am ready to save this template").AttachTo(download);

					download_text.style.margin = "1em";

					download.AttachTo(MyPagesInternal);

					var ss = new SaveFileAction();


					//ss.ToTransparentSprite();
					ss.AttachSpriteTo(MyPagesInternal);

					ss.AfterSave +=
						delegate
						{
							download_text.innerText = "I am ready to save this template";
						};

					ss.WhenReady(
						delegate
						{
							download_text.style.color = "blue";

							//download.disabled = false;


							download.onclick +=
								delegate
								{
									download_text.innerText = "Save by clicking here -> ";

									//var url = "data:text/html;base64," + ScriptCoreLib.JavaScript.Runtime.Convert.ToBase64String(n.InnerHTML);

									////data:image/gif;base64,R0lGODlhDwAPAKECAAAAzMzM
									//Native.Window.open(url, "_blank");

									ss.Add(n.InnerHTML, "Project1.csproj");
									ss.Add(n.InnerHTML, "My.UltraSource/Default.htm");

									// codebehind :)
									ss.Add(m1.Code1.value, "My.UltraSource/Default.htm.cs");

									ss.SaveFile("Project1");
								};
						}
					);
				}
				else if (Native.Document.location.hash == "#/UltraApplicationWithAssets")
				{
					new UltraApplicationWithAssets().Container.AttachToDocument();
				}
				else
					if (Native.Document.location.hash == "#/audio")
					{
						Action AtTimer = delegate { };

						(1000 / 15).AtInterval(
							tt =>
							{
								AtTimer();
							}
						);

						new SoundCloudBackground().Container.AttachTo(MyPagesBackground);
						new SoundCloudHeader().Container.AttachTo(MyPagesInternal);

						var page = 1;

						var Tracks = new IHTMLDiv().AttachTo(MyPagesInternal);
						Tracks.style.margin = "1em";

						var More = new SoundCloudMore();

						var AudioLinks = default(AudioLink);

						var LoadCurrentPage = default(Action);

						LoadCurrentPage = delegate
						{
							var loading = new SoundCloudLoading();

							loading.Container.AttachTo(Tracks);


							new UltraWebService().SoundCloudTracksDownload(
								System.Convert.ToString(page),
								ee =>
								{
									if (loading != null)
									{
										loading.Container.Orphanize();
										loading = null;
									}

									var t = new SoundCloudTrack();

									t.Content.ApplyToggleConcept(t.HideContent, t.ShowContent).Hide();

									t.Title.innerHTML = ee.trackName;
									t.Waveform.src = ee.waveformUrl;

									t.Audio.src = ee.streamUrl;
									t.Audio.autobuffer = true;


									AudioLinks = new AudioLink
									{
										Audio = t.Audio,
										Prev = AudioLinks
									};

									var _AudioLinks = AudioLinks;

									if (AudioLinks.Prev != null)
										AudioLinks.Prev.Next = AudioLinks;
									else
										// we are the first  :)
										t.Audio.play();

									t.MoreButton.onclick +=
										delegate
										{
											t.Audio.pause();

											if (_AudioLinks.Next != null)
											{
												_AudioLinks.Next.Audio.currentTime = 0;
												_AudioLinks.Next.Audio.play();

												if (_AudioLinks.Next.Next == null)
												{
													page++;
													LoadCurrentPage();
												}
											}
										};

									t.Audio.onended +=
										delegate
										{
											if (_AudioLinks.Next != null)
											{
												_AudioLinks.Next.Audio.currentTime = 0;
												_AudioLinks.Next.Audio.play();

												if (_AudioLinks.Next.Next == null)
												{
													page++;
													LoadCurrentPage();
												}
											}
										};

									t.Identity.innerText = ee.uid;

									t.Play.onclick += eee => { eee.PreventDefault(); t.Audio.play(); };
									t.Pause.onclick += eee => { eee.PreventDefault(); t.Audio.pause(); };

									t.Title.style.cursor = IStyle.CursorEnum.pointer;
									t.Title.onclick += eee =>
										{
											eee.PreventDefault();

											var playing = true;

											if (t.Audio.paused)
												playing = false;

											if (t.Audio.ended)
												playing = false;

											if (!playing)
												t.Audio.play();
											else
												t.Audio.pause();
										};

									DoubleAction SetProgress1 = p =>
									{

										t.Gradient3.style.width = System.Convert.ToInt32(800 * p) + "px";
										t.Gradient4.style.width = System.Convert.ToInt32(800 * p) + "px";
									};

									t.Gradient5.style.Opacity = 0.4;
									t.Gradient6.style.Opacity = 0.4;

									DoubleAction SetProgress2 = p =>
									{

										t.Gradient5.style.width = System.Convert.ToInt32(800 * p) + "px";
										t.Gradient6.style.width = System.Convert.ToInt32(800 * p) + "px";
									};

									AtTimer +=
										delegate
										{
											if (t.Audio.duration == 0)
											{
												t.Play.Hide();
												t.Pause.Hide();
												return;
											}
											else
											{

												var playing = true;

												if (t.Audio.paused)
													playing = false;

												if (t.Audio.ended)
													playing = false;

												if (!playing)
													t.Title.style.color = Color.None;
												else
													t.Title.style.color = Color.Blue;

												t.Play.Show(!playing);
												t.Pause.Show(playing);
											}

											var p = t.Audio.currentTime / t.Audio.duration;
											SetProgress1(p);
										};

									t.Waveform.onmouseout +=
										delegate
										{
											SetProgress2(0);
										};

									t.Waveform.onmousemove +=
										eee =>
										{
											SetProgress2(eee.OffsetX / 800.0);
										};

									t.Waveform.onclick +=
										eee =>
										{
											t.Audio.currentTime = t.Audio.duration * (eee.OffsetX / 800.0);
											t.Audio.play();
										};

									t.Waveform.style.cursor = IStyle.CursorEnum.pointer;

									SetProgress1(0);
									SetProgress2(0);

									t.Container.AttachTo(Tracks);
								}
							);


							10000.AtDelay(
								delegate
								{
									More.MoreButton.FadeIn(0, 1000, null);
								}
							);
						};


						More.MoreButton.Hide();
						More.Container.AttachTo(MyPagesInternal);

						More.MoreButton.onclick += eee =>
							{
								eee.PreventDefault();
								More.MoreButton.FadeOut(1, 300,
									delegate
									{
										page++;
										LoadCurrentPage();
									}
								);
							};

						LoadCurrentPage();

					}
					else
					{
						//new PromotionWebApplication1.HTML.Audio.FromAssets.Track1 { controls = true }.AttachToDocument();
						//new PromotionWebApplication1.HTML.Audio.FromWeb.Track1 { controls = true, autobuffer = true }.AttachToDocument();

						var IsAvalonJavaScript = hash == "#/avalon.js";
						var IsAvalonActionScript = hash == "#/avalon.as";
						var IsAvalon = IsAvalonActionScript || IsAvalonJavaScript;

						//if (IsAvalon)
						//{
						var ccc = new IHTMLDiv();

						ccc.style.position = IStyle.PositionEnum.absolute;
						ccc.style.left = "50%";
						ccc.style.top = "50%";
						ccc.style.marginLeft = (-AvalonLogoCanvas.DefaultWidth / 2) + "px";
						ccc.style.marginTop = (-AvalonLogoCanvas.DefaultHeight / 2) + "px";

						ccc.style.SetSize(AvalonLogoCanvas.DefaultWidth, AvalonLogoCanvas.DefaultHeight);

						ccc.AttachToDocument();

						if (IsAvalonActionScript)
						{
							var alof = new UltraSprite();
							alof.ToTransparentSprite();
							alof.AttachSpriteTo(ccc);
						}
						else
						{
							var alo = new AvalonLogoCanvas();
							alo.Container.AttachToContainer(ccc);
						}
						//}
						//else
						//{
						//    var cc = new HTML.Pages.FromAssets.Controls.Named.CenteredLogo_Kamma();

						//    cc.Container.AttachToDocument();

						//    // see: http://en.wikipedia.org/wiki/Perl_control_structures
						//    // "Unless" == "if not"  ;)

						//    IsMicrosoftInternetExplorer.YetIfNotThen(cc.TheLogoImage.BeginPulseAnimation).ButIfSoThen(cc.TheLogoImage.HideNowButShowAtDelay);
						//}

						var aa = new About();
						aa.Service.innerText = gapageview;
						aa.Container.AttachToDocument();

					}
			}
			#endregion



			"UA-13087448-1".ToGoogleAnalyticsTracker(
				pageTracker =>
				{
					pageTracker._setDomainName(".jsc-solutions.net");
					pageTracker._trackPageview(gapageview);
				}
			);


		}



		/// <summary>
		/// Microsoft Internet Explorer does not support using opacity on an image with an alpha layer.
		/// </summary>
		public static bool IsMicrosoftInternetExplorer
		{
			get
			{
				return (bool)new IFunction("/*@cc_on return true; @*/ return false;").apply(null);
			}
		}



	}


	public delegate void StringAction(string e);
	public delegate void StringActionAction(StringAction e);

	public sealed class UltraWebService : ISoundCloudTracksDownload
	{

		public void Hello(string data, StringAction result)
		{
			result(data + " hello");
			result(data + " world");
		}

		public void GetTitleFromServer(StringAction result)
		{
			var r = new Random();

			var Targets = new[]
			{
				"javascript",
				"java",
				"actionscript",
				"php"
			};

			result("jsc solutions - C# to " + Targets[r.Next(0, Targets.Length)]);

			// should we add timing information if we use Thread.Sleep to the results?

		}

		/*ISoundCloudTracksDownload. not supported yet ? */
		public void SoundCloudTracksDownload(string page, Services.SoundCloudTrackFound yield)
		{
			new Services.SoundCloudTracks().SoundCloudTracksDownload(page, yield);
		}
	}
}
