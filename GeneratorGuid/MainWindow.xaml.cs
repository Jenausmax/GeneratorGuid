using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace GeneratorGuid;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private bool _flag = false;
    private static List<string> _guidCollections = new();
    private static string _guid = "Guid generator \n";

    public MainWindow()
    {
        InitializeComponent();
        TextBox1.Text = "Guid generator \n";
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        _flag = true;

        while (_flag)
        {
            await Task.Delay(2000);
            var guid = Guid.NewGuid().ToString();

            _guidCollections.Add(guid);

            _guid = _guid + "\n" + guid;

            TextBox1.Text = _guid;
        }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        _flag = false;
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        
        var directory = $"{DateTime.Now.ToString("yy-MM-dd")}-gen";
        var file = $"{_guidCollections.Count}-count_{DateTime.Now.ToString("yy-MM-dd")}_{Guid.NewGuid().ToString("N")}.txt";

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        try
        {
            File.AppendAllLines($"{directory}\\{file}", _guidCollections);
            Process.Start(new ProcessStartInfo { FileName = $"{directory}\\{file}", UseShellExecute = true });
        }
        catch (Exception exception)
        {
            File.AppendAllText($"{directory}-log.txt", $"{exception.Data}\n{exception.Message} \n {exception.StackTrace}");
            throw;
        }

        Lab.Content = "Сохранены файлы!";
    }

    private void Button_Click_3(object sender, RoutedEventArgs e)
    {
        DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(Environment.CurrentDirectory);
        DirectoryInfo[] dirsInDir = hdDirectoryInWhichToSearch.GetDirectories("*" + "-gen");

        foreach (DirectoryInfo dir in dirsInDir)
        {
            if (Directory.Exists(dir.FullName))
            {
                Directory.Delete(dir.FullName, true);
            }
        }

        Lab.Content = "Удалены созданные файлы!";
    }

    private void Button_Click_Clear(object sender, RoutedEventArgs e)
    {
        _guid = "Guid generator \n";
    }
}

