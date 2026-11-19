using System.Diagnostics;
using System.Windows;
using PrimeLab;
namespace PrimeApp;

public partial class MainWindow : Window {
    private bool _busy;
    private int _clicks;
    public MainWindow() => InitializeComponent();
    private void Count_Click(object sender, RoutedEventArgs e) => ClickText.Text = $"{++_clicks} Klicks";
    private void Start_Click(object sender, RoutedEventArgs e) {
        if (_busy)
            return;
        if (!long.TryParse(StartBox.Text, out var start) || start < 0 || start > 1_000_000_000_000 ||
            !int.TryParse(CountBox.Text, out var count) || count < 1 || count > 1000) {
            StatusText.Text = "Bitte Start 0–1 000 000 000 000 und Anzahl 1–1000 eingeben.";
            return;
        }
        _busy = true;
        StartButton.IsEnabled = false;
        StartBox.IsEnabled = false;
        CountBox.IsEnabled = false;
        StatusText.Text = "Berechnung läuft …";
        var stopwatch = Stopwatch.StartNew();
        var uiThread = Environment.CurrentManagedThreadId;
        try {
            // TODO: CPU-Arbeit auslagern und asynchron abwarten.
            var values = Primes.Next(start, count);
            Results.ItemsSource = values;
            StatusText.Text = $"{values.Count} Primzahlen in {stopwatch.ElapsedMilliseconds} ms. UI-Thread {uiThread} → {Environment.CurrentManagedThreadId}.";
        } catch (Exception ex) { StatusText.Text = $"Berechnung fehlgeschlagen: {ex.Message}"; } finally {
            _busy = false;
            StartButton.IsEnabled = true;
            StartBox.IsEnabled = true;
            CountBox.IsEnabled = true;
        }
    }
}
