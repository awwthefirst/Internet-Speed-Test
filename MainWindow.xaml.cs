using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace Internet_Speed_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool isVisible = false;
        private int cooldown = 0;

        public MainWindow()
        {
            InitializeComponent();
            this.Hide();
            Timer timer = new Timer(10);
            timer.Elapsed += CheckIfKeysPressed;
            timer.Start();
        }

        private void CheckIfKeysPressed(Object source, ElapsedEventArgs e) //Maybe change keybinds
        {
            if (cooldown > 0)
            {
                cooldown--;
            }
            else
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    bool altPressed = Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt);
                    bool iPressed = Keyboard.IsKeyDown(Key.I);
                    if (altPressed && iPressed)
                    {
                        if (this.isVisible)
                            this.Hide();
                        else
                            this.Show();
                        this.isVisible = !this.isVisible;
                        cooldown = 30;
                    }
                });
            }
        }
    }
}
