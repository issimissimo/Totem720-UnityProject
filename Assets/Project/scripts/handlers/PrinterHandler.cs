using UnityEngine;

public class PrinterHandler : MonoBehaviour
{
    public void PrintBytes(byte[] bytes)
    {
        string printerName = Globals.data.stampante;

        if (string.IsNullOrEmpty(printerName))
        {
            ErrorManager.instance.ShowError(ErrorManager.TYPE.WARNING, "Non è stata specificata la stampante, quindi non è possibile stampare");
        }
        else
        {
            print("Printing on: " + printerName);
            LCPrinter.Print.PrintTexture(bytes, 1, printerName);
        }
    }
}

