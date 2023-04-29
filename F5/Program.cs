using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace F5 {
	internal class Program {
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		private static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

		private const uint WM_KEYDOWN = 0x0100;
		private const int VK_F5 = 0x74;

		static void Main(string[] args) {
			int minutes;
			while(true) {
				Console.WriteLine("How many minutes do you want to keep pressing F5?");
				string input = Console.ReadLine();
				if (!int.TryParse(input, out minutes)) {
					Console.WriteLine("Invalid input. Please enter an integer.");
					continue;
				}
				break;
			}

			Console.WriteLine("Program starting, will press F5 every 5 mins");
			while (true) {
				Thread.Sleep(minutes * 60 * 1000);
				Console.WriteLine($"[{DateTime.Now}] Pressing F5");

				// Get the handle of the foreground window
				IntPtr foregroundWindowHandle = GetForegroundWindow();

				// Send the F5 key press to the foreground window
				PostMessage(foregroundWindowHandle, WM_KEYDOWN, VK_F5, 0);
			}
		}
	}
}
