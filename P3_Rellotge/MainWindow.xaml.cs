using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace P3_Rellotge
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Alarm ObjAlarma = new Alarm(0,0,0,false);
        private List<DiferenciaHoraria> listCountry = new List<DiferenciaHoraria>();
        private DiferenciaHoraria selectedCountry = null;
        const string FileName = @"SavedAlarm.bin";
        const string Countries = @"Countries.bin";


        public MainWindow()
        {
            InitializeComponent();
            DataContext = ObjAlarma;
            if (File.Exists(FileName))
            {
                Stream TestFileStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                ObjAlarma = (Alarm)deserializer.Deserialize(TestFileStream);
                TestFileStream.Close();
            }
            if (File.Exists(Countries))
            {
                Stream TestFileStream = File.OpenRead(Countries);
                BinaryFormatter deserializer = new BinaryFormatter();
                listCountry = (List<DiferenciaHoraria>) deserializer.Deserialize(TestFileStream);
                TestFileStream.Close();
            }

            lblTime.Content = DateTime.Now.ToString("HH:mm:ss tt");
            
            printAlarmLabel();

            for (int i = 0; i < 24; i++)
            {
                cbHours.Items.Add(i);
            }

            for (int i = 0; i < 60; i++)
            {
                cbMinutes.Items.Add(i);
                cbSeconds.Items.Add(i);
            }


            cbHours.SelectedIndex = DateTime.Now.Hour;
            cbMinutes.SelectedIndex = 0;
            cbSeconds.SelectedIndex = 0;

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0,0,1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            lblTime.Content = datetime.ToString("HH:mm:ss tt");
            if (ObjAlarma.Actived)
            {
                DateTime now = DateTime.Parse(datetime.ToString("HH:mm:ss tt"));
                DateTime alarma = DateTime.Parse(getAlarmTime());
                TimeSpan diff = alarma.TimeOfDay - datetime.TimeOfDay;
                int millisceonds = (int)diff.TotalMilliseconds;
              
                Debug.WriteLine(millisceonds);
                lblAlarm.Content = "Alarm On - " + getAlarmTime() + " (remaining "+ diff.Duration().Hours + ":"+ diff.Duration().Minutes+ ":"+diff.Duration().Seconds+ ")";
                if (millisceonds <= 0 && millisceonds > -1000)
                {
                    MessageBox.Show("Es l'hora.");
                    ObjAlarma.Actived = false;
                    printAlarmLabel();
                }
            }

            if(selectedCountry != null)
            {
                lblSecundaryClock.Visibility = Visibility.Visible;
                DateTime secundaryClock = datetime.AddHours(selectedCountry.Diff);
                lblSecundaryClock.Content = secundaryClock.ToString("HH:mm:ss tt") + " ("+selectedCountry.Pais+")";
            }

            // code goes here
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ObjAlarma.Hour = int.Parse(cbHours.SelectedValue.ToString());
            ObjAlarma.Minutes = int.Parse(cbMinutes.SelectedValue.ToString());
            ObjAlarma.Seconds = int.Parse(cbSeconds.SelectedValue.ToString());

            Stream TestFileStream = File.Create(FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(TestFileStream, ObjAlarma);
            TestFileStream.Close();

            printAlarmLabel();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = ((MenuItem)sender);
            if (item.Name.Equals("activeAlarm"))
            {
                activeAlarm.IsChecked = true;
                deactiveAlarm.IsChecked = false;
                ObjAlarma.Actived = true;
                printAlarmLabel();
            }
            else if (item.Name.Equals("deactiveAlarm"))
            {
                activeAlarm.IsChecked = false;
                deactiveAlarm.IsChecked = true;
                ObjAlarma.Actived = false;
                printAlarmLabel();
            }
            else if (item.Name.Equals("addCountry"))
            {
                AddCountry inputDialog = new AddCountry("Nou país", "Australia", "-9");
                if (inputDialog.ShowDialog() == true)
                {
                    listCountry.Add(new DiferenciaHoraria(inputDialog.Name, int.Parse(inputDialog.Diff)));
                    Serialize(listCountry, Countries);
                }
                //lblName.Text = inputDialog.Answer;
            }
            else if (item.Name.Equals("delCountry"))
            {
                delCountry inputDialog = new delCountry(listCountry);
                if (inputDialog.ShowDialog() == true) {
                    listCountry.Remove(inputDialog.Country);
                    Serialize(listCountry, Countries);
                }
            }
        }

        private void printAlarmLabel()
        {
            if(ObjAlarma.Actived == true) {
                lblAlarm.Content = "Alarm On";
            }
            else{
                lblAlarm.Content = "Alarm Off";
            }

            lblAlarm.Content = lblAlarm.Content + " - " + getAlarmTime();
        }

        private String getAlarmTime()
        {
            return ObjAlarma.Hour + ":" + ObjAlarma.Minutes + ":" + ObjAlarma.Seconds;
        }

        public void Serialize(List<DiferenciaHoraria> list, string filePath)
        {
            using (Stream stream = File.OpenWrite(filePath))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, list);
            }
        }

        public List<DiferenciaHoraria> Deserialize(string filePath)
        {
            using (Stream stream = File.OpenRead(filePath))
            {
                var formatter = new BinaryFormatter();
                return (List<DiferenciaHoraria>)formatter.Deserialize(stream);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            selCountry inputDialog = new selCountry(listCountry);
            if (inputDialog.ShowDialog() == true)
            {
                selectedCountry = inputDialog.Country;
            }
        }
    }
}
