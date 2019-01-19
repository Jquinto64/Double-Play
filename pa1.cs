using System;
using static System.Console;

namespace Bme121
{
    static class Program
    {
        // Play the 'double play' sliding puzzle game.
        // This game idea comes from Larry D. Nichols and is found at the
        // URL http://www.ageofpuzzles.com/Puzzles/DoublePlay/DoublePlay.htm.

        static void Main( )
        {
            Random rGen = new Random( );

            // Try to figure out which form of the board to display.
            // Non-Windows machines seem to not have the box-drawing characters.

            bool useU0000 = false;
            if( Environment.OSVersion.Platform == PlatformID.MacOSX ) useU0000 = true;
            if( Environment.OSVersion.Platform == PlatformID.Unix ) useU0000 = true;

            // Initialize the game board in the solved puzzle state.
            // The zero value represents a hole.

            int[ , ] board =
            {
                {  0,  1,  2,  0 },
                {  3,  4,  5,  6 },
                {  7,  8,  9, 10 },
                {  0, 11, 12,  0 }
            };

            // Dimensions of the game board are extracted into variables for convenience.

            int rows = board.GetLength( 0 );
            int cols = board.GetLength( 1 );
            int length = board.Length;

            // This is the main game-playing loop.
            // Each iteration is either performing one random move (as part of scrambling)
            // or one move entered by the user.

            bool quit = false;
            int randomMoves = 0;
            int num=0;
            while( ! quit )
            {
                int move = 0;

                // Either generate a random move or display the game board and ask the user for a move.

                if( randomMoves > 0 )
                {
                    move = rGen.Next( 1, 13 );

                    randomMoves --;
                }
                else
                {
                    // Extract the game-board values into an array of displayed game-board strings.
                    // This is done so the strings can be of width 3 which makes the game-board
                    // display code below express very clearly.

                    string[ ] map = new string[ length ];
                    for( int i = 0; i < length; i ++ )
                    {
                        int value = board[ i / cols, i % cols ];
                        if( value == 0 ) map[ i ] = "   ";
                        else map[ i ] = $" {value:x} ";
                    }

                    // Display the game board.

                    Clear( );
                    WriteLine( );
                    WriteLine( " Welcome to the double-play game!" );
                    WriteLine( " Tiles slide in pairs by pushing towards a hole." );
                    WriteLine( " Scramble, then arrange back in order by sliding." );
                    WriteLine( );

                    if( useU0000 )
                    {
                        // Use Unicode 'C0 Controls and Basic Latin' range 0000–007f.

                        WriteLine( " +---+---+---+---+" );
                        WriteLine( " |{0}|{1}|{2}|{3}|", map[  0 ], map[  1 ], map[  2 ], map[  3 ] );
                        WriteLine( " +---+---+---+---+" );
                        WriteLine( " |{0}|{1}|{2}|{3}|", map[  4 ], map[  5 ], map[  6 ], map[  7 ] );
                        WriteLine( " +---+---+---+---+" );
                        WriteLine( " |{0}|{1}|{2}|{3}|", map[  8 ], map[  9 ], map[ 10 ], map[ 11 ] );
                        WriteLine( " +---+---+---+---+" );
                        WriteLine( " |{0}|{1}|{2}|{3}|", map[ 12 ], map[ 13 ], map[ 14 ], map[ 15 ] );
                        WriteLine( " +---+---+---+---+" );
                    }
                    else
                    {
                        // Use Unicode 'Box Drawing' range 2500–257f.

                        WriteLine( " ╔═══╦═══╦═══╦═══╗" );
                        WriteLine( " ║{0}║{1}║{2}║{3}║", map[  0 ], map[  1 ], map[  2 ], map[  3 ] );
                        WriteLine( " ╠═══╬═══╬═══╬═══╣" );
                        WriteLine( " ║{0}║{1}║{2}║{3}║", map[  4 ], map[  5 ], map[  6 ], map[  7 ] );
                        WriteLine( " ╠═══╬═══╬═══╬═══╣" );
                        WriteLine( " ║{0}║{1}║{2}║{3}║", map[  8 ], map[  9 ], map[ 10 ], map[ 11 ] );
                        WriteLine( " ╠═══╬═══╬═══╬═══╣" );
                        WriteLine( " ║{0}║{1}║{2}║{3}║", map[ 12 ], map[ 13 ], map[ 14 ], map[ 15 ] );
                        WriteLine( " ╚═══╩═══╩═══╩═══╝" );
                    }
                    WriteLine( );

                    // Interpret the user's desired move.

                    Write( " Tile to push (s to scramble, q to quit): " );
                    string response = ReadKey( intercept: true ).KeyChar.ToString( );
                    WriteLine( );

                    switch( response )
                    {
                        case "s": randomMoves = 100_000; break;

                        case "1": move =  1; break;
                        case "2": move =  2; break;
                        case "3": move =  3; break;
                        case "4": move =  4; break;
                        case "5": move =  5; break;
                        case "6": move =  6; break;
                        case "7": move =  7; break;
                        case "8": move =  8; break;
                        case "9": move =  9; break;
                        case "a": move = 10; break;
                        case "b": move = 11; break;
                        case "c": move = 12; break;

                        case "q": quit = true; break;
                    }
                }

                // If a move is possible, adjust the game board to make the move.

                if( move > 0 )
                {
					// boolean used to exit second mother if statement so moves are not made twice
					bool movePos=false;
			
					// Find which key was pressed
					for (int row = 0; row <= 3; row++) //Search rows for pressed key
		            {
		                for (int column = 0; column <= 3; column++) //Search columns for pressed key
		                {
					        if (board[row, column] == move && movePos==false) //only executed when boolean is false                   
							{
								// Horizontal movements established by the following 4 if statements
								if (column == 0 && board[row, 1] != 0 && board[row, 2] == 0) // Look at column 0 in an arbitrary row; column 1 to its right must not be 0, and column 2 must be 0
								{
									// Rightward movement
									board[row, 2] = board[row, 1]; // Tile in column 2, row x first moves horizontally to location of hole
									board[row, 1] = board[row, 0]; // Tile column 1, row x moves horizontally next, to where column 2, row x was
									board[row, 0] = 0; // original tile selected becomes 0 
									num++;
									movePos = true; // boolean changes to true so for loops that search for selected move do not continue once a move a is made
								}
								// Above repeats for next three if horizontal statements
								else if (column == 1 && board[row, 2] != 0 && board[row, 3] == 0)
								{
									//Rightward movement
									board[row, 3] = board[row, 2];
									board[row, 2] = board[row, 1];
									board[row, 1] = 0;
									num++;
									movePos = true;
								}
								else if (column == 3 && board[row, 2] != 0 && board[row, 1] == 0)
								{
									// Leftward movement
									board[row, 1] = board[row, 2];
									board[row, 2] = board[row, 3];
									board[row, 3] = 0;
									num++;
									movePos=true;
								}
								else if (column == 2 && board[row, 1] != 0 && board[row, 0] == 0)
								{
									//Leftward movement
									board[row, 0] = board[row, 1];
									board[row, 1] = board[row, 2];
									board[row, 2] = 0;
									num++;
									movePos = true;
								}
								//Vertical, column-wise moves begin here; same as above, but row and column are inverted 
								else if (row == 0 && board[1, column] != 0 && board[2, column] == 0) 
								{
									// Upward movement
									board[2, column] = board[1, column];
									board[1, column] = board[0, column];
									board[0, column] = 0;
									num++;
									movePos = true;
								}
								else if (row == 1 && board[2, column] != 0 && board[3, column] == 0)
								{
									// Upward movement
									board[3, column] = board[2, column];
									board[2, column] = board[1, column];
									board[1, column] = 0;
									num++;
									movePos = true;
								}
								else if (row == 3 && board[2, column] != 0 && board[1, column] == 0)
								{
									// Downward movement
									board[1, column] = board[2, column];
									board[2, column] = board[3, column];
									board[3, column] = 0;
									num++;
									movePos = true;
								}
								else if (row == 2 && board[1, column] != 0 && board[0, column] == 0)
								{
									// Downward movement
									board[0, column] = board[1, column];
									board[1, column] = board[2, column];
									board[2, column] = 0;
									num++;
									movePos = true;
								}
							}
						}
					}
                }
            }
            WriteLine( " Thanks for playing!" );
            //~ WriteLine("{0} moves were made",num);//move counter
            WriteLine( );
        }
    }
}
