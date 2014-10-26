using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.provider;
using android.view;
using android.webkit;
using android.widget;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Windows.Forms;
using FormsShowDialog.Library;

namespace FormsShowDialog.Activities
{
    // targetSdkVersion
    // http://developer.android.com/guide/topics/manifest/uses-sdk-element.html
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    public class ApplicationActivity : Activity
    {



        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            var r = default(global::ScriptCoreLib.Android.Windows.Forms.IAssemblyReferenceToken_Forms);
            ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext = this;


 //           Implementation not found for type import :
 //type: System.Drawing.SizeF
 //           method: Void.ctor(Single, Single)
 //           Did you forget to add the[Script] attribute?
 //           Please double check the signature!


            // X:\jsc.svn\examples\java\android\forms\FormsMessageBox\FormsMessageBox\Library\ApplicationControl.cs

            // cmd /K c:\util\android-sdk-windows\platform-tools\adb.exe logcat
            // Camera PTP

            // http://developer.android.com/guide/topics/ui/notifiers/notifications.html

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);


            var b = new android.widget.Button(this);

            // jsc is doing the wrong thing here
            var SDK_INT = android.os.Build.VERSION.SDK_INT;

            b.setText("Notify! " + new { SDK_INT, android.os.Build.VERSION.SDK });
            int counter = 0;

            b.AtClick(
                delegate
                {
                    counter++;



                    var f = new Form1();

                    var value = f.ShowDialog();

                    b.setText("ShowDialog! " + new { value });
                }
            );

            ll.addView(b);

            this.setContentView(sv);
        }
    }


}
