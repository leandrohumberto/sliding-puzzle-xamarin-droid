using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Views;
using System.Collections;
using System;
using Android.Content;

namespace AndroidSlidingPuzzleCSharp
{
    [Activity(Label = "AndroidSlidingPuzzleCSharp", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button resetButton;
        GridLayout mainLayout;
        int gameViewWidth;
        int tileWidth;
        ArrayList tilesArray;
        ArrayList coordsArray;
        Point emptySpot;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            resetButton = FindViewById<Button>(Resource.Id.resetButtonId);
            resetButton.Click += Reset;

            mainLayout = FindViewById<GridLayout>(Resource.Id.gameGridLayoutId);
            gameViewWidth = Resources.DisplayMetrics.WidthPixels;

            SetGameView();
            MakeTiles();
        }

        private void MakeTiles()
        {
            // Calculate the width/height of the tiles
            tileWidth = gameViewWidth / 4;

            // Initialize ArrayLists
            tilesArray = new ArrayList();
            coordsArray = new ArrayList();

            // Counter for the tile numbers
            int count = 1;

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    // Create the tile (TextTileView)
                    TextTileView textTile = new TextTileView(this);

                    // Set the tile with the layout parameter
                    textTile.LayoutParameters = GetTileLayoutParams(row, col);

                    // Change the color
                    textTile.SetBackgroundColor(Color.Green);

                    // Set the text
                    textTile.Text = count++.ToString();
                    textTile.SetTextColor(Color.Black);
                    textTile.TextSize = 40;
                    textTile.Gravity = GravityFlags.Center;

                    // Subscribe to the tile's Touch event handler
                    textTile.Touch += TileTouched;

                    // Add the tile to the game's grid layout
                    mainLayout.AddView(textTile);

                    // Keep the tile and its coordenate in the arrays
                    tilesArray.Add(textTile);
                    coordsArray.Add(new Point(row, col));
                }
            }

            mainLayout.RemoveView((TextTileView)tilesArray[15]);
            tilesArray.RemoveAt(15);
            Randomize();
        }

        private void TileTouched(object sender, View.TouchEventArgs e)
        {
            if (e.Event.Action == MotionEventActions.Up)
            {
                TextTileView view = (TextTileView)sender;
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);

                dialog.SetMessage($"Touched Position: X = {view.PositionX}, Y = {view.PositionY}\n\n" 
                    + $"Empty Position X = {emptySpot.X}, Y = {emptySpot.Y}\n\n");
                dialog.SetNeutralButton("OK", delegate { });
                dialog.Show();
            }
        }

        private void Randomize()
        {
            ArrayList tempArray = new ArrayList(coordsArray);
            Random random = new Random();

            foreach (TextTileView view in tilesArray)
            {
                int position = random.Next(0, tempArray.Count);
                Point coord = (Point)tempArray[position];
                view.LayoutParameters = GetTileLayoutParams(coord.X, coord.Y);
                view.PositionX = coord.X;
                view.PositionY = coord.Y;

                tempArray.RemoveAt(position);
            }

            emptySpot = (Point)tempArray[0];
        }

        private GridLayout.LayoutParams GetTileLayoutParams(int x, int y)
        {
            // Create the specifications that establish in which row and column the tile is going to be rendered
            GridLayout.Spec rowSpec = GridLayout.InvokeSpec(x);
            GridLayout.Spec colSpec = GridLayout.InvokeSpec(y);

            // Create a new layout parameter object for the tile using the previous specs
            GridLayout.LayoutParams tileLayoutParams = new GridLayout.LayoutParams(rowSpec, colSpec);
            tileLayoutParams.Width = tileWidth - 10;
            tileLayoutParams.Height = tileWidth - 10;
            tileLayoutParams.SetMargins(5, 5, 5, 5);
            return tileLayoutParams;
        }

        private void SetGameView()
        {
            mainLayout.ColumnCount = 4;
            mainLayout.RowCount = 4;
            mainLayout.LayoutParameters = new LinearLayout.LayoutParams(gameViewWidth, gameViewWidth);
            mainLayout.SetBackgroundColor(Color.Gray);
        }

        private void Reset(object sender, EventArgs e)
        {
            Randomize();
        }

        private class TextTileView : TextView
        {
            public int PositionX { get; set; }
            public int PositionY { get; set; }

            public TextTileView(Context context) : base(context)
            {
            }
        }
    }
}

