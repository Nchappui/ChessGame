using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;
using System.Threading.Tasks;
//using Cinemachine;

public class GameState : MonoBehaviour
{
    private GameObject currentlySelectedPiece = null;
    public bool isWhiteTurn = true; // Flag to check if it's white's turn
    public GameObject playerOneCamera;
    public GameObject playerTwoCamera;

    public ParticleSystem fireWorks;

    private bool castleLeft = false;
    private bool castleRight = false;

    private bool gameFinished = false;

    public float timerDuration = 60f; // Default timer duration in seconds

    public GameObject lastMovedPiece = null;

    public GameObject timerCanvas;

    private StockfishClient stockfishClient;

    public bool hasTimerGame = false; // Flag to check if the game has a timer
    public bool isAIGame = false; // Flag to check if the game is an AI game

    private FunctionHelper functionHelper;

    public int chosenSkillLevel = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
        GameManager gm = GameManager.Instance;
        functionHelper = GetComponent<FunctionHelper>();
        isAIGame = gm.isAIGame; // Get the AI game flag from GameManager
        hasTimerGame = gm.hasTimerGame; // Get the timer game flag from GameManager
        timerDuration = gm.timerDuration; // Get the timer duration from GameManager
        chosenSkillLevel = gm.chosenSkillLevel; // Get the chosen skill level from GameManager
        if (isAIGame)
        {
            LaunchStockfishAsync(); // Launch Stockfish if it's an AI game
        }
        if (hasTimerGame)
        {
            timerCanvas.SetActive(true); // Activate the timer canvas if the game has a timer
        }
        else
        {
            timerCanvas.SetActive(false); // Deactivate the timer canvas if the game does not have a timer
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameFinished)
        {
            playerOneCamera.transform.RotateAround(new Vector3(3.5f,0.7f,3.5f), Vector3.up, 20 * Time.deltaTime); 
            playerTwoCamera.transform.RotateAround(new Vector3(3.5f,0.7f,3.5f), Vector3.up, 20 * Time.deltaTime);
        }


    }

    public GameObject getCurrentlySelectedPiece()
    {
        return currentlySelectedPiece;
    }
    
    public void LaunchStockfishAsync()
    {
        stockfishClient = FindFirstObjectByType<StockfishClient>();
        if (stockfishClient == null)
        {
            Debug.LogError("StockfishClient not found in the scene.");
            return;
        }
    }

    public void selectPiece(GameObject piece, List<Case> availableMoves, List<GameObject> attackablePieces, List<GameObject> rooksToCastle = null)
    {
        if (piece.GetComponent<Outline>().OutlineColor == Color.red)
        {
            // If the piece is attackable, move it

            if (piece.GetComponent<KingPiece>() != null)
            {
                UIUtils UIutils = FindAnyObjectByType<UIUtils>().GetComponent<UIUtils>();
                if (piece.GetComponent<ChessPiece>().team == ChessPiece.Team.White)
                {
                    UIutils.ShowWinnerPanel(ChessPiece.Team.Black);
                }
                else
                {
                    UIutils.ShowWinnerPanel(ChessPiece.Team.White);

                }
                fireWorks.Play();
                gameFinished = true;
            }
            piece.gameObject.SetActive(false);
            Destroy(piece.gameObject);
            MovePiece(piece.GetComponent<ChessPiece>().getCurrentCase().transform);
            return;
        }
        if (piece.GetComponent<ChessPiece>().team == ChessPiece.Team.Black && isWhiteTurn ||
        piece.GetComponent<ChessPiece>().team == ChessPiece.Team.White && !isWhiteTurn)
        {
            return; // If it's white's turn and the piece is black, do nothing
        }
        if (currentlySelectedPiece == null)
        {
            currentlySelectedPiece = piece;
        }
        else if (currentlySelectedPiece == piece)
        {
            return;
        }
        else
        {

            currentlySelectedPiece.GetComponent<ChessPiece>().deselectPiece();
            currentlySelectedPiece = piece;

        }

        currentlySelectedPiece.GetComponent<ChessPiece>().selectPiece(availableMoves, attackablePieces, rooksToCastle);
    }

    public void MovePiece(Transform newPlace)
    {
        bool diagMove = false;

        if (currentlySelectedPiece == null)
        {
            return;
        }
        else
        {
            if (currentlySelectedPiece.GetComponent<KingPiece>() != null && currentlySelectedPiece.GetComponent<ChessPiece>().getCurrentCase().E != null && currentlySelectedPiece.GetComponent<ChessPiece>().getCurrentCase().E.E != null && newPlace == currentlySelectedPiece.GetComponent<ChessPiece>().getCurrentCase().E.E.transform)
            {
                Debug.Log("King is castling right");
                castleRight = true;
            }
            else if (currentlySelectedPiece.GetComponent<KingPiece>() != null && currentlySelectedPiece.GetComponent<ChessPiece>().getCurrentCase().W != null && currentlySelectedPiece.GetComponent<ChessPiece>().getCurrentCase().W.W != null && newPlace == currentlySelectedPiece.GetComponent<ChessPiece>().getCurrentCase().W.W.transform)
            {
                Debug.Log("King is castling left");
                castleLeft = true;
            }
            else
            {
                castleLeft = false;
                castleRight = false;
            }

            if (castleRight)
            {
                GameObject rook = currentlySelectedPiece.GetComponent<ChessPiece>().getCurrentCase().E.E.E.currentPiece;
                rook.transform.position = new Vector3(
                    currentlySelectedPiece.GetComponent<ChessPiece>().getCurrentCase().E.transform.position.x,
                    rook.transform.position.y,
                    rook.transform.position.z);
                rook.GetComponent<ChessPiece>().setNewCase();
                rook.GetComponent<ChessPiece>().deselectPiece();
            }
            else if (castleLeft)
            {
                GameObject rook = currentlySelectedPiece.GetComponent<ChessPiece>().getCurrentCase().W.W.W.W.currentPiece;
                rook.transform.position = new Vector3(
                    currentlySelectedPiece.GetComponent<ChessPiece>().getCurrentCase().W.transform.position.x,
                    rook.transform.position.y,
                    rook.transform.position.z);
                rook.GetComponent<ChessPiece>().setNewCase();
                rook.GetComponent<ChessPiece>().deselectPiece();
            }

            if (currentlySelectedPiece.GetComponent<PawnPiece>() != null && Vector3.Distance(currentlySelectedPiece.transform.position, newPlace.position) > 1.1f && Vector3.Distance(currentlySelectedPiece.transform.position, newPlace.position) < 1.9f)
            {
                diagMove = true; // If the pawn moved diagonally, set diagMove to true
            }


            currentlySelectedPiece.transform.position = new Vector3(
            newPlace.position.x,
            currentlySelectedPiece.transform.position.y,
            newPlace.position.z);

            currentlySelectedPiece.GetComponent<ChessPiece>().setNewCase();
            currentlySelectedPiece.GetComponent<ChessPiece>().deselectPiece(diagMove);
            lastMovedPiece = currentlySelectedPiece; // Store the last moved piece
            currentlySelectedPiece = null;
            isWhiteTurn = !isWhiteTurn; // Switch turns after a move
            if (!gameFinished && !isAIGame)
            {
                playerOneCamera.SetActive(isWhiteTurn); // Activate player one camera if it's white's turn
                playerTwoCamera.SetActive(!isWhiteTurn); // Activate player two camera if it's black's turn
            }
            else if (!gameFinished && isAIGame && !isWhiteTurn)
            {
                functionHelper.ComputeFEN();
                _ = functionHelper.GetStockfishMove(stockfishClient, chosenSkillLevel);
            }

        }
    }
}
