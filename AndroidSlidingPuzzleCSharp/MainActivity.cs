using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;

namespace AndroidSlidingPuzzleCSharp
{
    [Activity(Label = "AndroidSlidingPuzzleCSharp", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button resetButton;
        GridLayout mainLayout;
        int gameViewWidth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            resetButton = FindViewById<Button>(Resource.Id.resetButtonId);
            resetButton.Click += ResetMethod;

            mainLayout = FindViewById<GridLayout>(Resource.Id.gameGridLayoutId);
            gameViewWidth = Resources.DisplayMetrics.WidthPixels;
            SetGameView();
        }

        private void SetGameView()
        {
            mainLayout.ColumnCount = 4;
            mainLayout.RowCount = 4;
            mainLayout.LayoutParameters = new LinearLayout.LayoutParams(gameViewWidth, gameViewWidth);
            mainLayout.SetBackgroundColor(Color.Gray);
        }

        private void ResetMethod(object sender, System.EventArgs e)
        {
            
        }
    }
}

