using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FunctionHelper : MonoBehaviour
{
    public GameObject chessBoard;
    private List<Case> cases = new List<Case>();

    private string fen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 1; i < chessBoard.transform.childCount; i++)
        {
            Transform child = chessBoard.transform.GetChild(i);
            if (child.gameObject.GetComponent<Case>() != null)
            {
                cases.Add(child.gameObject.GetComponent<Case>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ComputeFEN()
    {
        string temp = "";
        int emptyCount = 0;

        for (int i = 0; i < cases.Count; i++)
        {
            Case currentCase = cases[i];
            if (currentCase.currentPiece == null)
            {
                emptyCount++;
            }
            else
            {
                ChessPiece piece = currentCase.currentPiece.GetComponent<ChessPiece>();
                if (emptyCount > 0)
                {
                    temp += emptyCount.ToString();
                    emptyCount = 0;
                }
                if (piece.team == ChessPiece.Team.White)
                {
                    temp += char.ToUpper(piece.fenChar); // Use uppercase for white pieces
                }
                else
                {
                    temp += piece.fenChar;
                }
            }

            // Add a slash at the end of each row
            if ((i + 1) % 8 == 0)
            {
                if (emptyCount > 0)
                {
                    temp += emptyCount.ToString();
                    emptyCount = 0;
                }
                temp += "/";
            }
        }

        // Remove the last slash
        if (temp.EndsWith("/"))
        {
            temp = temp.Substring(0, temp.Length - 1);
        }

        temp += " b KQkq - 0 1"; // Add the turn, castling rights, en passant target square, halfmove clock, and fullmove number
        fen = temp;

        Debug.Log("FEN: " + temp);
    }
    
    public async Task GetStockfishMove(StockfishClient stockfishClient, int chosenSkillLevel)
    {

        // Example of how to use Stockfish to get a move
        string move = await stockfishClient.GetBestMove(fen, 4, chosenSkillLevel);
        Debug.Log("Proposed move by Stockfish: " + move);
    }
}
