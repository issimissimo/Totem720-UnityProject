
/*
 
    -----------------------
    UDP-Receive (send to)
    -----------------------
    // [url]http://msdn.microsoft.com/de-de/library/bb979228.aspx#ID0E3BAC[/url]
   
   
    // > receive
    // 127.0.0.1 : 8051
   
    // send
    // nc -u 127.0.0.1 8051
 
*/
using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour
{
    // receiving Thread
    Thread receiveThread;

    // udpclient object
    UdpClient client;

    // public
    // public string IP = "127.0.0.1"; default local
    public int port = 8051; // define > init

    // infos
    private string lastReceivedUDPPacket = "";
    private string allReceivedUDPPackets = ""; // clean up this from time to time!


    private void Update()
    {
        if (lastReceivedUDPPacket != "")
        {
            ShowPanelOnMessageReceived(lastReceivedUDPPacket);
            lastReceivedUDPPacket = "";
        }
    }


    private void ShowPanelOnMessageReceived(string msg)
    {
        print("received message...");
        string[] array = msg.Split(' ');

        if (array.Length != 2)
        {
            ErrorManager.instance.ShowError(ErrorManager.TYPE.WARNING, "Nel messaggio bisogna definire sia lo scenario che la squadra");
        }
        else
        {
            if (Utils.ValidateString(array[0]) && Utils.ValidateString(array[1]))
            {
                try
                {
                    Globals.Scenario scenario = (Globals.Scenario)System.Enum.Parse(typeof(Globals.Scenario), array[0]);
                    Globals.Squadra squadra = (Globals.Squadra)System.Enum.Parse(typeof(Globals.Squadra), array[1]);

                    if (GameManager.STATE == GameManager.GAMESTATE.IDLE)
                    {
                        GameManager.instance.ShowPanelByType(scenario, squadra);
                    }
                    else
                    {
                        ErrorManager.instance.ShowError(ErrorManager.TYPE.WARNING, "Non si può cambiare scenario e squadra durante il gioco");
                    }
                }
                catch (Exception e)
                {
                    ErrorManager.instance.ShowError(ErrorManager.TYPE.WARNING, "Lo scenario e la squadra definiti nel messaggio non sono stati riconosciuti");
                }
            }
        }
    }

    // private IEnumerator _OnMessageReceived(string msg){
    //     while (GameManager.STATE != GameManager.GAMESTATE.IDLE){
    //         yield return null;
    //     }
    // }



    // start from shell
    private static void Main()
    {
        UDPReceive receiveObj = new UDPReceive();
        receiveObj.init();

        string text = "";
        do
        {
            text = Console.ReadLine();
        }
        while (!text.Equals("exit"));
    }
    // start from unity3d
    public void Start()
    {

        init();

    }

    // OnGUI
    // void OnGUI()
    // {
    //     Rect rectObj = new Rect(40, 10, 200, 400);
    //     GUIStyle style = new GUIStyle();
    //     style.alignment = TextAnchor.UpperLeft;
    //     GUI.Box(rectObj, "# UDPReceive\n127.0.0.1 " + port + " #\n"
    //                 + "shell> nc -u 127.0.0.1 : " + port + " \n"
    //                 + "\nLast Packet: \n" + lastReceivedUDPPacket
    //                 + "\n\nAll Messages: \n" + allReceivedUDPPackets
    //             , style);
    // }

    // init
    private void init()
    {
        // Endpunkt definieren, von dem die Nachrichten gesendet werden.
        // print("UDPSend.init()");

        // define port
        // port = 8051;

        // status
        // print("Sending to 127.0.0.1 : " + port);
        // print("Test-Sending to this Port: nc -u 127.0.0.1  " + port + "");


        // ----------------------------
        // Abhören
        // ----------------------------
        // Lokalen Endpunkt definieren (wo Nachrichten empfangen werden).
        // Einen neuen Thread für den Empfang eingehender Nachrichten erstellen.
        receiveThread = new Thread(
            new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

    }

    // receive thread
    private void ReceiveData()
    {

        client = new UdpClient(port);
        while (true)
        {

            try
            {
                // Bytes empfangen.
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);

                // Bytes mit der UTF8-Kodierung in das Textformat kodieren.
                string text = Encoding.UTF8.GetString(data);

                // Den abgerufenen Text anzeigen.
                print(">> " + text);


                // latest UDPpacket
                lastReceivedUDPPacket = text;

                // ....
                allReceivedUDPPackets = allReceivedUDPPackets + text;

            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }


    void OnApplicationQuit()
    {

        if (receiveThread.IsAlive)
        {
            receiveThread.Abort();
        }
        // receiver.Close();
    }

    // getLatestUDPPacket
    // cleans up the rest
    public string getLatestUDPPacket()
    {
        allReceivedUDPPackets = "";
        return lastReceivedUDPPacket;
    }
}