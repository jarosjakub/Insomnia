using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsInput;
using WindowsInput.Native;

namespace Insomnia
{
    public class Write
    {
        public static float cycle = 21600f;
        public static int sleep = 1000;

        public void Setup()
        {
            Console.WriteLine("Enter the amount of cycles for the runtime:");
            try
            {
                Write.cycle = float.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Default settings will be used" + "\n");
            }

            Console.WriteLine("Enter the duration of one cycle in seconds:");
            try
            {
                var seconds = int.Parse(Console.ReadLine());
                Write.sleep = seconds * 1000;
            }
            catch (Exception f)
            {
                Console.WriteLine("Default settings will be used" + "\n");
            }



            Console.WriteLine("This will run for " + Write.cycle * Write.sleep / 1000 + " seconds, i.e. " + Write.cycle * Write.sleep / 1000 / 60 + " minutes, i.e. " + Write.cycle * Write.sleep / 1000 / 60 / 60 + " hours.");
        }
        public void Execute()
        {
            using (FileStream fs = File.Create("C:\\Windows\\Temp\\insomnia.txt")) ;

            var notepad = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "C:\\Windows\\Temp\\insomnia.txt",
                    UseShellExecute = true
                }
            };

            notepad.Start();

            [DllImport("user32.dll")]
            static extern bool SetForegroundWindow(IntPtr hWnd);

            [DllImport("user32.dll")]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


            var sim = new InputSimulator();

            System.Threading.Thread.Sleep(2000);
            IntPtr hWnd = FindWindow(null, "insomnia.txt - Notepad");

            if (hWnd == IntPtr.Zero)
            {
                Console.WriteLine("Window not found!");
                return;
            }
            var i = 0;


            // Set the window as foreground
            SetForegroundWindow(hWnd);

            while (i < Write.cycle)
            {
                System.Threading.Thread.Sleep(Write.sleep);
                sim.Keyboard.KeyPress(VirtualKeyCode.VK_M);
                i++;
            }


            notepad.Kill();
            File.Delete("C:\\Windows\\Temp\\insomnia.txt");
        }
    }
}