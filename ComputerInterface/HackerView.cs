using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using ComputerInterface;
using ComputerInterface.ViewLib;

namespace HackerTyper.ComputerInterface
{
    class HackerView : ComputerView
    {
        private int currentHack = 0;
        private string fullHack = "";
        private string url = "https://raw.githubusercontent.com/fchb1239/HackerTyper/main/Web/Hack.txt";

        public HackerView()
        {
            GetHack();
        }

        public override void OnShow(object[] args)
        {
            base.OnShow(args);
            // changing the Text property will fire an PropertyChanged event
            // which lets the computer know the text has changed and update it
            UpdateScreen();
        }

        private void UpdateScreen()
        {
            SetText(str =>
            {
                // Set colour to green for the epic hacker feel
                str.Append("<color=green>");

                if (fullHack.Length > 0)
                {
                    // Generate text
                    string a = "";
                    for (int i = 0; i < currentHack; i++)
                    {
                        char c = fullHack[i];

                        a += c;

                        if (a.Split('\n').ToList().Last().Length >= SCREEN_WIDTH)
                            a += '\n';

                        if (a.Split('\n').Length >= SCREEN_HEIGHT)
                            a = a.Remove(0, a.Split('\n')[0].Length + 1);
                    }

                    // Set text
                    str.Append(a);
                }
                else
                {
                    str.Append("Logging into mainframe");
                }

                // Unset colour for some reason
                str.Append("</color>");
            });
        }

        public override void OnKeyPressed(EKeyboardKey key)
        {
            switch (key)
            {
                case EKeyboardKey.Back:
                    ReturnToMainMenu();
                    break;
                default:
                    currentHack += UnityEngine.Random.Range(2, 5);
                    if (currentHack > fullHack.Length)
                        currentHack = 0;

                    UpdateScreen();
                    break;
            }
        }

        private async void GetHack()
        {
            // Request setup
            HttpClient client = new HttpClient();

            Log("Creating HttpRequestMessage");

            // Send
            HttpRequestMessage msg = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };

            Log("Sending request");

            var response = await client.SendAsync(msg);

            // Receive
            Log($"Received response: {response.StatusCode}");

            // Check status
            if (response.IsSuccessStatusCode)
            {
                //Log(response.Content.ReadAsStringAsync().Result);
                fullHack = response.Content.ReadAsStringAsync().Result;
            }
            else
                fullHack = $"string status = \"Failed to load\";\nstring httpResponse = \"{response.StatusCode}\";";

            UpdateScreen();
        }

        private void Log(string str)
        {
            Console.WriteLine($"Hacker Typer - {DateTime.Now} : {str}");
        }
    }
}
