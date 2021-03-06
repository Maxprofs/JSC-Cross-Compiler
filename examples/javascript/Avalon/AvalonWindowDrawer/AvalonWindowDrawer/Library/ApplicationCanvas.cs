using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Windows.Input;

namespace AvalonWindowDrawer.Library
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();
        public Base.ApplicationCanvas c;
        public ApplicationCanvas()
        {
            c = new Base.ApplicationCanvas();

            c.Selection.GlassArea.Orphanize();
            c.Selection.Orphanize();



            c.AttachTo(this);
            c.MoveTo(8, 8);

            this.SizeChanged += (s, e) => c.SizeTo(this.Width - 16.0, this.Height - 16.0);

            r.Fill = Brushes.Red;
            r.Opacity = 0;

            r.AttachTo(this);
            r.MoveTo(8, 8);

            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);

            Rectangle h = null;
            AnimatedOpacity<Rectangle> hOpacity = null;


            Action<Action<double, double>> GetPosition = null;
            var Windows = new List<Base.ApplicationCanvas.WindowInfo>();

         
            #region GetSnapLocation
            Action<Func<UIElement, Point>, Action<bool, double, double, double, double>> GetSnapLocation =
                (e_GetPosition, SetLocation) =>
                {
                    var p = e_GetPosition(r);

                    var x = 0.0;
                    var y = 0.0;

                    GetPosition((_x, _y) => { x = _x; y = _y; });

                    var cx = p.X - x;
                    var cy = p.Y - y;

                    if (cx < 0)
                    {
                        x += cx;
                        cx = -cx;
                    }

                    if (cy < 0)
                    {
                        y += cy;
                        cy = -cy;
                    }

                    var Snap = 16;
                    var SnapMode = false;

                    Enumerable.FirstOrDefault(
                        from k in Windows
                        let dx0 = Math.Abs(k.WindowLocation.Left - x)
                        where dx0 < Snap
                        orderby dx0
                        select k
                    ).With(
                     ax =>
                     {
                         SnapMode = true;
                         cx += x - ax.WindowLocation.Left;
                         x = ax.WindowLocation.Left;
                     }
                   );


                    Enumerable.FirstOrDefault(
                         from k in Windows
                         let dx0 = Math.Abs(k.WindowLocation.Top - y)
                         where dx0 < Snap
                         orderby dx0
                         select k
                     ).With(
                      ax =>
                      {
                          SnapMode = true;
                          cy += y - ax.WindowLocation.Top;
                          y = ax.WindowLocation.Top;
                      }
                    );


                    SetLocation(SnapMode, x, y, cx, cy);
                };
            #endregion

            #region MouseLeftButtonDown
            r.MouseLeftButtonDown +=
               (s, e) =>
               {

                   h = new Rectangle
                   {
                       Fill = Brushes.Black,
                   };
                   hOpacity = h.ToAnimatedOpacity();
                   hOpacity.Opacity = 0.3;

                   var p = e.GetPosition(r);

                   //Console.WriteLine("MouseLeftButtonDown " + new { p.X, p.Y });

                   GetPosition = y => y(p.X, p.Y);


                   h.AttachTo(c).MoveTo(p).SizeTo(0, 0);

                   c.Selection.Orphanize();

                   c.Selection.WindowLocation.Left = p.X;
                   c.Selection.WindowLocation.Top = p.Y;
                   c.Selection.WindowLocation.Width = 0;
                   c.Selection.WindowLocation.Height = 0;
                   c.Selection.WindowLocationChanged();
                   c.Selection.Attach();


                   Windows.WithEach(k => k.GlassOpacity.Opacity = 0);
               };
            #endregion



            #region MouseMove
            r.MouseMove +=
               (s, e) =>
               {
                   if (GetPosition != null)
                   {



                       GetSnapLocation(e.GetPosition,
                           (SnapMode, x, y, cx, cy) =>
                           {
                               //Console.WriteLine("MouseMove " + new { x, y, cx, cy });

                               if (SnapMode)
                                   hOpacity.Opacity = 0.9;
                               else
                                   hOpacity.Opacity = 0.3;

                               c.Selection.WindowLocation.Left = x;
                               c.Selection.WindowLocation.Top = y;
                               c.Selection.WindowLocation.Width = cx;
                               c.Selection.WindowLocation.Height = cy;
                               c.Selection.WindowLocationChanged();

                               h.MoveTo(x, y).SizeTo(cx, cy);
                           }
                       );
                   }
               };
            #endregion


            #region MouseLeftButtonUp
            r.MouseLeftButtonUp +=
                 (s, e) =>
                 {
                     //Console.WriteLine("MouseLeftButtonUp");

                     if (GetPosition == null)
                         return;


                     GetSnapLocation(e.GetPosition,
                        (SnapMode, x, y, cx, cy) =>
                        {
                            //Console.WriteLine("MouseLeftButtonUp " + new { x, y, cx, cy });

                            Windows.WithEach(k => k.GlassOpacity.Opacity = 1);

                            h.Orphanize();
                            c.Selection.Orphanize();

                            if (cx > 32)
                                if (cy > 32)
                                    c.CreateWindow(
                                        new Base.ApplicationCanvas.Position
                                        {
                                            Left = x,
                                            Top = y,
                                            Width = cx,
                                            Height = cy
                                        },

                                       Windows.Add
                                     );


                        }
                    );



                     GetPosition = null;


                 };
            #endregion



            #region TouchDown
            r.TouchDown +=
               (s, e) =>
               {
                   
                   h = new Rectangle
                   {
                       Fill = Brushes.Black,
                   };
                   hOpacity = h.ToAnimatedOpacity();
                   hOpacity.Opacity = 0.3;

                   var p = e.GetTouchPoint(r).Position;

                   //Console.WriteLine("MouseLeftButtonDown " + new { p.X, p.Y });

                   GetPosition = y => y(p.X, p.Y);


                   h.AttachTo(c).MoveTo(p).SizeTo(0, 0);

                   c.Selection.Orphanize();

                   c.Selection.WindowLocation.Left = p.X;
                   c.Selection.WindowLocation.Top = p.Y;
                   c.Selection.WindowLocation.Width = 0;
                   c.Selection.WindowLocation.Height = 0;
                   c.Selection.WindowLocationChanged();
                   c.Selection.Attach();


                   Windows.WithEach(k => k.GlassOpacity.Opacity = 0);
               };
            #endregion



            #region MouseMove
            r.TouchMove +=
               (s, e) =>
               {
                   if (GetPosition != null)
                   {

                       Func<UIElement, Point> e_GetPosition = x => e.GetTouchPoint(x).Position;

                       GetSnapLocation(e_GetPosition,
                           (SnapMode, x, y, cx, cy) =>
                           {
                               //Console.WriteLine("MouseMove " + new { x, y, cx, cy });

                               if (SnapMode)
                                   hOpacity.Opacity = 0.9;
                               else
                                   hOpacity.Opacity = 0.3;

                               c.Selection.WindowLocation.Left = x;
                               c.Selection.WindowLocation.Top = y;
                               c.Selection.WindowLocation.Width = cx;
                               c.Selection.WindowLocation.Height = cy;
                               c.Selection.WindowLocationChanged();

                               h.MoveTo(x, y).SizeTo(cx, cy);
                           }
                       );
                   }
               };
            #endregion


            #region TouchUp
            r.TouchUp +=
                 (s, e) =>
                 {
                     //Console.WriteLine("MouseLeftButtonUp");

                     if (GetPosition == null)
                         return;

                     Func<UIElement, Point> e_GetPosition = x => e.GetTouchPoint(x).Position;

                     GetSnapLocation(e_GetPosition,
                        (SnapMode, x, y, cx, cy) =>
                        {
                            //Console.WriteLine("MouseLeftButtonUp " + new { x, y, cx, cy });

                            Windows.WithEach(k => k.GlassOpacity.Opacity = 1);

                            h.Orphanize();
                            c.Selection.Orphanize();

                            if (cx > 32)
                                if (cy > 32)
                                    c.CreateWindow(
                                        new Base.ApplicationCanvas.Position
                                        {
                                            Left = x,
                                            Top = y,
                                            Width = cx,
                                            Height = cy
                                        },

                                       Windows.Add
                                     );


                        }
                    );



                     GetPosition = null;


                 };
            #endregion

        }

    }
}
