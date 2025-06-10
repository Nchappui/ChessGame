using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class StockfishClient : MonoBehaviour
{
    private Process stockfishProcess;
    private StreamWriter stockfishInput;
    private StreamReader stockfishOutput;

    public string stockfishPath = "Assets/Plugins/Stockfish/stockfish.exe";

    void Start()
    {
        StartStockfish();
    }

    void OnDestroy()
    {
        if (stockfishProcess != null && !stockfishProcess.HasExited)
            stockfishProcess.Kill();
    }

    public void StartStockfish()
    {
        stockfishProcess = new Process();
        stockfishProcess.StartInfo.FileName = stockfishPath;
        stockfishProcess.StartInfo.UseShellExecute = false;
        stockfishProcess.StartInfo.RedirectStandardInput = true;
        stockfishProcess.StartInfo.RedirectStandardOutput = true;
        stockfishProcess.StartInfo.CreateNoWindow = true;
        stockfishProcess.Start();

        stockfishInput = stockfishProcess.StandardInput;
        stockfishOutput = stockfishProcess.StandardOutput;
    }

    public async Task<string> GetBestMove(string fen, int depth = 15)
    {
        stockfishInput.WriteLine("uci");
        stockfishInput.WriteLine($"position fen {fen}");
        stockfishInput.WriteLine($"go depth {depth}");

        string bestMove = null;
        while (true)
        {
            string line = await stockfishOutput.ReadLineAsync();
            if (line.StartsWith("bestmove"))
            {
                bestMove = line.Split(' ')[1];
                break;
            }
        }
        return bestMove;
    }
}