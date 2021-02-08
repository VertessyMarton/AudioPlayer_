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
using Microsoft.Win32;
using System.IO;
using System.Windows.Threading;

namespace AudioPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private MediaPlayer mediaplayer = new MediaPlayer();
        List<string> zenék = new List<string>();
        private bool asd = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "MP3 Files (*.mp3)|*.mp3|All Files (*.*)|*.*";
            open.ShowDialog();
            if (open.ShowDialog() == true)
            {
                mediaplayer.Open(new Uri(open.FileName));
                screen.Items.Add(open.FileName);
            }

        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            mediaplayer.Play();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            mediaplayer.Stop();
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            mediaplayer.Pause();
        }

        private void volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaplayer.Volume = volume.Value;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mediaplayer.Source != null) && (mediaplayer.NaturalDuration.HasTimeSpan) && (!asd))
            {
                csuszka.Minimum = 0;
                csuszka.Maximum = mediaplayer.NaturalDuration.TimeSpan.TotalSeconds;
                csuszka.Value = mediaplayer.Position.TotalSeconds;
            }
        }

        private void timer_TextChanged(object sender, TextChangedEventArgs e)
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            timer.Text = TimeSpan.FromSeconds(csuszka.Value).ToString(@"hh\:mm\:ss");
            mediaplayer.Position = TimeSpan.FromSeconds(csuszka.Value);
        }

        private void resume_Click(object sender, RoutedEventArgs e)
        {
            mediaplayer.Play();
        }

        private void open1_Click(object sender, RoutedEventArgs e)
        {
            if (screen.SelectedItems.Count != 0)
            {
                mediaplayer.Open(new Uri(zenék[screen.SelectedIndex - 1]));
                csuszka.Minimum = 0;
                csuszka.Maximum = Convert.ToDouble(mediaplayer.NaturalDuration.TimeSpan.TotalSeconds);
                csuszka.Value = Convert.ToDouble(mediaplayer.Position.TotalSeconds);
            }
            else
            {
                mediaplayer.Stop();
                mediaplayer.Play();
            }
            mediaplayer.Play();
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (screen.SelectedItems.Count != 0)
            {
                if (screen.SelectedIndex == screen.Items.Count)
                {
                    mediaplayer.Open(new Uri(zenék[0]));
                    csuszka.Minimum = 0;
                    csuszka.Maximum = Convert.ToDouble(mediaplayer.NaturalDuration.TimeSpan.TotalSeconds);
                    csuszka.Value = Convert.ToDouble(mediaplayer.Position.TotalSeconds);
                }
                else
                {
                    mediaplayer.Open(new Uri(zenék[screen.SelectedIndex + 1]));
                }
            }
            mediaplayer.Play();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (screen.SelectedItems.Count != 0)
            {
                while (screen.SelectedIndex != -1)
                {
                    screen.Items.RemoveAt(screen.SelectedIndex);
                }
            }
        }
    }
}
