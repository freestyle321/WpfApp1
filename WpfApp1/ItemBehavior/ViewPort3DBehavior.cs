using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;

namespace WpfApp1.ItemBehavior
{
    public class ViewPort3DBehavior : Behavior<Viewport3D>
    {
        PerspectiveCamera myPCamera;
        Viewport3D Viewport3D;
        Point mouseLastPosition;
        double mouseDeltaFactor = 0.4;
        public ViewPort3DBehavior()
        {
            myPCamera = new PerspectiveCamera();
            myPCamera.Position = new Point3D(0, 0, 200);
            myPCamera.LookDirection = new Vector3D(0, 0, -1);
            myPCamera.FieldOfView = 1000;
            Viewport3D=(Viewport3D)this.AssociatedObject;
            Viewport3D.Camera = myPCamera;
        }
        protected override void OnAttached()
        {

            Viewport3D.MouseEnter += Vp_MouseEnter;
            Viewport3D.MouseLeave += Vp_MouseLeave;
            Viewport3D.MouseWheel += Viewport3D_MouseWheel;
            Viewport3D.MouseMove += Viewport3D_MouseMove;
            Viewport3D.MouseLeftButtonDown += Viewport3D_MouseLeftButtonDown;
            base.OnAttached();
        }

        private void Viewport3D_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window mainwin = Application.Current.MainWindow;
            mouseLastPosition = e.GetPosition(mainwin);
            //RayHitTestParameters hitParams = new RayHitTestParameters(myPCamera.Position, myPCamera.LookDirection);
            //VisualTreeHelper.HitTest(vp.Children[0], null, ResultCallback, hitParams);

            //下面是进行点击触发检测，可忽略，注释
            Point3D testpoint3D = new Point3D(mouseLastPosition.X, mouseLastPosition.Y, 0);
            Vector3D testdirection = new Vector3D(mouseLastPosition.X, mouseLastPosition.Y, 100);
            PointHitTestParameters pointparams = new PointHitTestParameters(mouseLastPosition);
            RayHitTestParameters rayparams = new RayHitTestParameters(testpoint3D, testdirection);

            //test for a result in the Viewport3D
            VisualTreeHelper.HitTest(Viewport3D, null, HTResult, pointparams);
        }

        private void Viewport3D_MouseMove(object sender, MouseEventArgs e)
        {

            Window mainwin = Application.Current.MainWindow;
              if (Mouse.LeftButton == MouseButtonState.Pressed)//如果按下鼠标左键

            {

                Point newMousePosition = e.GetPosition(mainwin);

                if (mouseLastPosition.X != newMousePosition.X)

                {
                    //进行水平旋转
                    HorizontalTransform(mouseLastPosition.X < newMousePosition.X, mouseDeltaFactor);//水平变换

                }

                if (mouseLastPosition.Y != newMousePosition.Y)// change position in the horizontal direction

                {
                    //进行垂直旋转
                    VerticalTransform(mouseLastPosition.Y > newMousePosition.Y, mouseDeltaFactor);//垂直变换 

                }

                mouseLastPosition = newMousePosition;

            }
        }

        private void Viewport3D_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double scaleFactor = 13;
            //120 near ,   -120 far
            System.Diagnostics.Debug.WriteLine(e.Delta.ToString());
            Point3D currentPosition = myPCamera.Position;
            Vector3D lookDirection = myPCamera.LookDirection;//new Vector3D(camera.LookDirection.X, camera.LookDirection.Y, camera.LookDirection.Z);
            lookDirection.Normalize();

            lookDirection *= scaleFactor;

            if (e.Delta == 120)//getting near
            {
                //myPCamera.FieldOfView /= 1.2;

                if ((currentPosition.X + lookDirection.X) * currentPosition.X > 0)
                {
                    currentPosition += lookDirection;
                }
            }
            if (e.Delta == -120)//getting far
            {
                //myPCamera.FieldOfView *= 1.2;
                currentPosition -= lookDirection;
            }

            Point3DAnimation positionAnimation = new Point3DAnimation();
            positionAnimation.BeginTime = new TimeSpan(0, 0, 0);
            positionAnimation.Duration = TimeSpan.FromMilliseconds(100);
            positionAnimation.To = currentPosition;
            positionAnimation.From = myPCamera.Position;
            positionAnimation.Completed += new EventHandler(positionAnimation_Completed);
            myPCamera.BeginAnimation(PerspectiveCamera.PositionProperty, positionAnimation, HandoffBehavior.Compose);
        }
        void positionAnimation_Completed(object sender, EventArgs e)
        {
            Point3D position = myPCamera.Position;
            myPCamera.BeginAnimation(PerspectiveCamera.PositionProperty, null);
            myPCamera.Position = position;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
        private void Vp_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Viewport3D)sender).Effect = null;
        }
        private void Vp_MouseEnter(object sender, MouseEventArgs e)
        {
            DropShadowEffect BlurRadius = new DropShadowEffect();

            BlurRadius.BlurRadius = 20;
            BlurRadius.Color = Colors.Yellow;
            BlurRadius.Direction = 0;
            BlurRadius.Opacity = 1;
            BlurRadius.ShadowDepth = 0;
            ((Viewport3D)sender).Effect = BlurRadius;
        }

        private void VerticalTransform(bool upDown, double angleDeltaFactor)
        {
            Vector3D postion = new Vector3D(myPCamera.Position.X, myPCamera.Position.Y, myPCamera.Position.Z);
            Vector3D rotateAxis = Vector3D.CrossProduct(postion, myPCamera.UpDirection);
            RotateTransform3D rt3d = new RotateTransform3D();
            AxisAngleRotation3D rotate = new AxisAngleRotation3D(rotateAxis, angleDeltaFactor * (upDown ? 1 : -1));
            rt3d.Rotation = rotate;
            Matrix3D matrix = rt3d.Value;
            Point3D newPostition = matrix.Transform(myPCamera.Position);
            myPCamera.Position = newPostition;
            myPCamera.LookDirection = new Vector3D(-newPostition.X, -newPostition.Y, -newPostition.Z);

            //update the up direction
            Vector3D newUpDirection = Vector3D.CrossProduct(myPCamera.LookDirection, rotateAxis);
            newUpDirection.Normalize();
            myPCamera.UpDirection = newUpDirection;
        }
        // 水平变换：
        private void HorizontalTransform(bool leftRight, double angleDeltaFactor)
        {
            Vector3D postion = new Vector3D(myPCamera.Position.X, myPCamera.Position.Y, myPCamera.Position.Z);
            Vector3D rotateAxis = myPCamera.UpDirection;
            RotateTransform3D rt3d = new RotateTransform3D();
            AxisAngleRotation3D rotate = new AxisAngleRotation3D(rotateAxis, angleDeltaFactor * (leftRight ? 1 : -1));
            rt3d.Rotation = rotate;
            Matrix3D matrix = rt3d.Value;
            Point3D newPostition = matrix.Transform(myPCamera.Position);
            myPCamera.Position = newPostition;
            myPCamera.LookDirection = new Vector3D(-newPostition.X, -newPostition.Y, -newPostition.Z);
        }

        public HitTestResultBehavior HTResult(HitTestResult rawresult)
        {
            //MessageBox.Show(rawresult.ToString());
            // RayHitTestResult rayResult = rawresult as RayHitTestResult;
            RayHitTestResult rayResult = rawresult as RayHitTestResult;
            if (rayResult != null)
            {
                //RayMeshGeometry3DHitTestResult rayMeshResult = rayResult as RayMeshGeometry3DHitTestResult;
                RayHitTestResult rayMeshResultrayResult = rayResult as RayHitTestResult;
                if (rayMeshResultrayResult != null)
                {
                    //GeometryModel3D hitgeo = rayMeshResult.ModelHit as GeometryModel3D;
                    var visual3D = rawresult.VisualHit as ModelVisual3D;

                    //do something

                }
            }

            return HitTestResultBehavior.Continue;
        }

    }
}
