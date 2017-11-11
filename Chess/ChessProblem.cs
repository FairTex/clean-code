using System;

namespace Chess
{
    
	public class ChessProblem
	{
		private static Board board;
		public static ChessStatus[,] ChessStatus;

		public static void LoadFrom(string[] lines)
		{
			board = new BoardParser().ParseBoard(lines);
            ChessStatus = new ChessStatus[2, 2]
            {
                {Chess.ChessStatus.Stalemate, Chess.ChessStatus.Ok},
                {Chess.ChessStatus.Mate, Chess.ChessStatus.Check }
            };
		}

		// Определяет мат, шах или пат белым.
		public static ChessStatus GetChessStatus(PieceColor color = PieceColor.White)
		{
		    var isCheck = Convert.ToInt32(IsCheckFor(color)); 
		    var hasMove = Convert.ToInt32(HasMove(color));
		    return ChessStatus[isCheck, hasMove];
		}

	    private static bool HasMove(PieceColor color)
	    {
	        var hasMove = false;
	        foreach (var locFrom in board.GetPieces(color))
	        {
	            foreach (var locTo in board.GetPiece(locFrom).GetMoves(locFrom, board))
	            {
	                using (new TemporaryPieceMove(board, locFrom, locTo, board.GetPiece(locTo)))
	                {
	                    if (!IsCheckFor(color))
	                        return true;
                    }
	            }
	        }
	        return false;
	    }
        

	    private static bool IsCheckFor(PieceColor color)
	    {
	        var oppositeColor = InvertColor(color);

            foreach (var loc in board.GetPieces(oppositeColor))
	        {
	            var piece = board.GetPiece(loc);
	            var moves = piece.GetMoves(loc, board);
	            foreach (var destination in moves)
	            {
	                if (board.GetPiece(destination).Is(color, PieceType.King))
                        return true;
	            }
	        }
	        return false;
	    }

	    private static PieceColor InvertColor(PieceColor color)
	    {
	        return color == PieceColor.Black ? PieceColor.White : PieceColor.Black;
	    }

	    // check — это шах
		private static bool IsCheckForWhite()
		{
			var isCheck = false;
			foreach (var loc in board.GetPieces(PieceColor.Black))
			{
				var piece = board.GetPiece(loc);
				var moves = piece.GetMoves(loc, board);
				foreach (var destination in moves)
				{
					if (board.GetPiece(destination).Is(PieceColor.White, PieceType.King))
						isCheck = true;
				}
			}
			if (isCheck) return true;
			return false;
		}
	}
}