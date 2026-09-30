using System.Globalization;
using GMUEduTrans.Desktop.Models;
using GMUEduTrans.Desktop.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI;

namespace GMUEduTrans.Desktop;

public sealed class MainWindow : Window
{
    private readonly LocalDataStore _store = new();
    private readonly AppState _state;
    private readonly Grid _body = new();
    private readonly TextBlock _pageTitle = new();
    private readonly TextBlock _status = new();

    private static readonly CultureInfo Id = CultureInfo.GetCultureInfo("id-ID");
    private static readonly SolidColorBrush BrandGreen = new(ColorHelper.FromArgb(255, 28, 130, 2));
    private static readonly SolidColorBrush BrandGreenDark = new(ColorHelper.FromArgb(255, 18, 82, 20));
    private static readonly SolidColorBrush BrandGold = new(ColorHelper.FromArgb(255, 206, 150, 3));
    private static readonly SolidColorBrush Surface = new(ColorHelper.FromArgb(255, 248, 249, 246));
    private static readonly SolidColorBrush White = new(ColorHelper.FromArgb(255, 255, 255, 255));
    private static readonly SolidColorBrush TextPrimary = new(ColorHelper.FromArgb(255, 28, 34, 30));
    private static readonly SolidColorBrush TextMuted = new(ColorHelper.FromArgb(255, 98, 108, 101));
    private static readonly SolidColorBrush BorderBrush = new(ColorHelper.FromArgb(255, 226, 231, 224));

    public MainWindow()
    {
        Title = "GMU EduTrans ERP";
        _state = _store.Load();
        Content = BuildShell();
        ShowDashboard();
    }

    private UIElement BuildShell()
    {
        var root = new Grid { Background = Surface };
        root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(252) });
        root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        var sidebarBorder = new Border
        {
            Background = BrandGreenDark,
            Padding = new Thickness(16, 22, 16, 18)
        };
        var sidebar = new StackPanel { Spacing = 5 };

        var brand = new StackPanel { Margin = new Thickness(6, 2, 6, 20) };
        try
        {
            var logoPath = Path.Combine(AppContext.BaseDirectory, "Assets", "GMU-EduTrans-Logo.png");
            if (File.Exists(logoPath))
            {
                brand.Children.Add(new Image
                {
                    Source = new BitmapImage(new Uri(logoPath)),
                    Width = 178,
                    Height = 92,
                    Stretch = Stretch.Uniform,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(0, 0, 0, 4)
                });
            }
        }
        catch
        {
            brand.Children.Add(new TextBlock
            {
                Text = "GMU EduTrans",
                Foreground = BrandGold,
                FontSize = 24,
                FontWeight = Microsoft.UI.Text.FontWeights.Bold
            });
        }
        brand.Children.Add(new TextBlock
        {
            Text = "ERP DESKTOP",
            Foreground = new SolidColorBrush(ColorHelper.FromArgb(190, 255, 255, 255)),
            FontSize = 11,
            CharacterSpacing = 140,
            Margin = new Thickness(1, 4, 0, 0)
        });
        sidebar.Children.Add(brand);

        AddNav(sidebar, "Dashboard", "dashboard", ShowDashboard);
        AddNav(sidebar, "Booking & Quotation", "booking", ShowBookings);
        AddNav(sidebar, "Program Edukasi", "program", ShowPrograms);
        AddNav(sidebar, "Sales & CRM", "sales", ShowSales);
        AddNav(sidebar, "Operasional", "operations", ShowOperations);
        AddNav(sidebar, "Keuangan", "finance", ShowFinance);
        AddNav(sidebar, "SDM & Payroll", "hr", ShowHr);
        AddNav(sidebar, "Laporan", "reports", ShowReports);
        AddNav(sidebar, "Master Data", "master", ShowMaster);
        AddNav(sidebar, "Pengaturan", "settings", ShowSettings);

        var account = new Border
        {
            Background = new SolidColorBrush(ColorHelper.FromArgb(28, 255, 255, 255)),
            CornerRadius = new CornerRadius(10),
            Padding = new Thickness(12, 10, 12, 10),
            Margin = new Thickness(0, 18, 0, 0)
        };
        account.Child = new TextBlock
        {
            Text = "OWNER / DIRECTOR\nGMU EduTrans",
            Foreground = White,
            FontSize = 12,
            LineHeight = 18
        };
        sidebar.Children.Add(account);

        sidebarBorder.Child = sidebar;
        Grid.SetColumn(sidebarBorder, 0);
        root.Children.Add(sidebarBorder);

        var main = new Grid { Background = Surface };
        main.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        main.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        var headerBorder = new Border
        {
            Background = White,
            BorderBrush = BorderBrush,
            BorderThickness = new Thickness(0, 0, 0, 1),
            Padding = new Thickness(30, 20, 30, 17)
        };
        var header = new Grid();
        header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        header.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        var titleStack = new StackPanel { Spacing = 3 };
        _pageTitle.FontSize = 28;
        _pageTitle.FontWeight = Microsoft.UI.Text.FontWeights.SemiBold;
        _pageTitle.Foreground = TextPrimary;
        titleStack.Children.Add(_pageTitle);
        titleStack.Children.Add(new TextBlock
        {
            Text = "PT. Garsyani Multi Usaha • GMU EduTrans",
            Foreground = TextMuted,
            FontSize = 12
        });
        header.Children.Add(titleStack);

        var statusBorder = new Border
        {
            Background = new SolidColorBrush(ColorHelper.FromArgb(24, 28, 130, 2)),
            BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(70, 28, 130, 2)),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(18),
            Padding = new Thickness(12, 6, 12, 6),
            VerticalAlignment = VerticalAlignment.Center
        };
        _status.Text = "Siap";
        _status.Foreground = BrandGreenDark;
        _status.FontSize = 12;
        _status.FontWeight = Microsoft.UI.Text.FontWeights.SemiBold;
        statusBorder.Child = _status;
        Grid.SetColumn(statusBorder, 1);
        header.Children.Add(statusBorder);

        headerBorder.Child = header;
        Grid.SetRow(headerBorder, 0);
        main.Children.Add(headerBorder);

        _body.Margin = new Thickness(30, 24, 30, 30);
        Grid.SetRow(_body, 1);
        main.Children.Add(_body);

        Grid.SetColumn(main, 1);
        root.Children.Add(main);
        return root;
    }

    private void AddNav(StackPanel panel, string label, string module, Action action)
    {
        if (!RoleAccessService.CanAccess(SessionService.Current.Role, module)) return;

        var button = new Button
        {
            Content = label,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            HorizontalContentAlignment = HorizontalAlignment.Left,
            Foreground = White,
            Background = new SolidColorBrush(ColorHelper.FromArgb(0, 255, 255, 255)),
            BorderThickness = new Thickness(0),
            CornerRadius = new CornerRadius(8),
            Margin = new Thickness(0, 2, 0, 2),
            Padding = new Thickness(13, 10, 13, 10)
        };
        button.Click += (_, _) => action();
        panel.Children.Add(button);
    }

    private void SetPage(string title, UIElement content)
    {
        _pageTitle.Text = title;
        _body.Children.Clear();
        _body.Children.Add(content);
        _status.Text = "Tersimpan lokal";
    }

    private ScrollViewer Scroll(UIElement content) => new()
    {
        Content = content,
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto
    };

    private StackPanel PageStack() => new()
    {
        Spacing = 12
    };

    private TextBox Box(string header, string value = "", string placeholder = "") => new()
    {
        Header = header,
        Text = value,
        PlaceholderText = placeholder,
        Margin = new Thickness(0, 2, 0, 4)
    };

    private ComboBox Combo(string header, params string[] values)
    {
        var c = new ComboBox
        {
            Header = header,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Margin = new Thickness(0, 2, 0, 4)
        };
        foreach (var v in values) c.Items.Add(v);
        c.SelectedIndex = values.Length > 0 ? 0 : -1;
        return c;
    }

    private Button Primary(string text, RoutedEventHandler handler)
    {
        var b = new Button
        {
            Content = text,
            Foreground = White,
            Background = BrandGreen,
            BorderBrush = BrandGreen,
            CornerRadius = new CornerRadius(8),
            Padding = new Thickness(18, 10, 18, 10),
            Margin = new Thickness(0, 8, 0, 4)
        };
        b.Click += handler;
        return b;
    }

    private Border Card(string title, string detail, Action? delete = null)
    {
        var p = new StackPanel { Spacing = 4 };
        p.Children.Add(new TextBlock { Text = title, FontSize = 17, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold });
        p.Children.Add(new TextBlock { Text = detail, TextWrapping = TextWrapping.Wrap, Opacity = 0.72 });
        if (delete is not null)
        {
            var b = new Button { Content = "Hapus", Margin = new Thickness(0, 6, 0, 0) };
            b.Click += (_, _) => delete();
            p.Children.Add(b);
        }

        return new Border
        {
            Child = p,
            Background = White,
            BorderBrush = BorderBrush,
            Padding = new Thickness(16),
            CornerRadius = new CornerRadius(12),
            BorderThickness = new Thickness(1),
            Margin = new Thickness(0, 4, 0, 4)
        };
    }

    private void Persist(string message)
    {
        _store.Save(_state);
        _status.Text = message;
    }

    private void ShowDashboard()
    {
        var panel = PageStack();
        var participants = _state.Bookings.Where(x => x.Status != "Batal").Sum(x => x.Participants);
        var active = _state.Bookings.Count(x => x.Status is "Confirmed" or "Quotation");
        var income = _state.Finances.Where(x => x.Type == "Pemasukan").Sum(x => x.Amount);
        var expense = _state.Finances.Where(x => x.Type == "Pengeluaran").Sum(x => x.Amount);
        var net = income - expense;

        panel.Children.Add(new TextBlock
        {
            Text = $"Target bulan ini: {_state.TargetPax:N0} pax • Tercatat: {participants:N0} pax",
            FontSize = 18,
            Margin = new Thickness(0, 0, 0, 8)
        });
        panel.Children.Add(Card("Booking Aktif", active.ToString("N0", Id)));
        panel.Children.Add(Card("Sales Pipeline", $"{_state.Leads.Count:N0} lead"));
        panel.Children.Add(Card("Pemasukan", $"Rp {income.ToString("N0", Id)}"));
        panel.Children.Add(Card("Pengeluaran", $"Rp {expense.ToString("N0", Id)}"));
        panel.Children.Add(Card("Saldo Operasional", $"Rp {net.ToString("N0", Id)}"));
        panel.Children.Add(Card("Kegiatan Operasional", $"{_state.Operations.Count:N0} item"));
        SetPage("Dashboard", Scroll(panel));
    }

    private void ShowBookings()
    {
        var panel = PageStack();
        panel.Children.Add(new TextBlock { Text = "Tambah Booking", FontSize = 20, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold });
        var date = Box("Tanggal", DateTime.Today.ToString("yyyy-MM-dd"));
        var school = Box("Sekolah / Customer", placeholder: "Nama sekolah atau lembaga");
        var program = Box("Program", placeholder: "Contoh: Edukasi Stasiun");
        var pax = Box("Jumlah Peserta", placeholder: "20");
        var status = Combo("Status", "Draft", "Quotation", "Confirmed", "Selesai", "Batal");
        panel.Children.Add(date); panel.Children.Add(school); panel.Children.Add(program); panel.Children.Add(pax); panel.Children.Add(status);
        panel.Children.Add(Primary("Simpan Booking", async (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(school.Text) || string.IsNullOrWhiteSpace(program.Text) || !int.TryParse(pax.Text, out var n) || n < 1)
            {
                _status.Text = "Lengkapi customer, program, dan jumlah peserta.";
                return;
            }
            var booking = new BookingRecord
            {
                Date = date.Text.Trim(),
                School = school.Text.Trim(),
                Program = program.Text.Trim(),
                Participants = n,
                Status = status.SelectedItem?.ToString() ?? "Draft"
            };
            _state.Bookings.Add(booking);
            Persist("Booking tersimpan • menyiapkan arsip Drive");

            try
            {
                var drive = new DriveArchiveService();
                var folder = await drive.EnsureOrderFolderAsync(
                    "WIN-" + booking.Id,
                    booking.School,
                    booking.Date);
                _status.Text = string.IsNullOrWhiteSpace(folder.FolderUrl)
                    ? "Booking tersimpan • arsip Drive siap"
                    : "Booking tersimpan • arsip Drive terhubung";
            }
            catch (Exception ex)
            {
                _status.Text = "Booking tersimpan • sinkronisasi Drive tertunda: " + ex.Message;
            }
            ShowBookings();
        }));

        panel.Children.Add(new TextBlock { Text = "Daftar Booking", FontSize = 20, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold, Margin = new Thickness(0, 14, 0, 0) });
        foreach (var x in _state.Bookings.OrderByDescending(x => x.Date).ToList())
            panel.Children.Add(Card($"{x.Date} • {x.School}", $"{x.Program} • {x.Participants} pax • {x.Status}", () =>
            {
                _state.Bookings.RemoveAll(y => y.Id == x.Id);
                Persist("Booking dihapus");
                ShowBookings();
            }));
        SetPage("Booking & Quotation", Scroll(panel));
    }

    private void ShowPrograms()
    {
        var panel = PageStack();
        panel.Children.Add(new TextBlock
        {
            Text = "Master program ringkas. Harga dan HPP produksi tetap harus mengikuti master ERP resmi.",
            TextWrapping = TextWrapping.Wrap,
            Opacity = 0.72
        });
        panel.Children.Add(Card("Edukasi Stasiun", "Baseline publik: Rp46.000/pax • Minimum 20 pax • Durasi ±2 jam"));
        panel.Children.Add(Card("Edukasi Kereta Api", "Program edukasi perjalanan dan profesi perkeretaapian."));
        panel.Children.Add(Card("Edukasi Padi Pandanwangi", "Program edukasi lokal Cianjur."));
        panel.Children.Add(Card("Edukasi Batik", "Program edukasi budaya dan aktivitas kreatif."));
        panel.Children.Add(Card("Tiket Rombongan", "Layanan tiket rombongan sekolah/lembaga, termasuk KA jarak jauh dan layanan yang didukung."));
        SetPage("Program Edukasi", Scroll(panel));
    }

    private void ShowSales()
    {
        var panel = PageStack();
        panel.Children.Add(new TextBlock { Text = "Tambah Lead", FontSize = 20, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold });
        var school = Box("Sekolah / Lembaga");
        var contact = Box("Kontak");
        var stage = Combo("Tahap", "Prospek", "Follow Up", "Quotation", "Negosiasi", "Closing", "Lost");
        var potential = Box("Potensi Nilai (Rp)", placeholder: "0");
        panel.Children.Add(school); panel.Children.Add(contact); panel.Children.Add(stage); panel.Children.Add(potential);
        panel.Children.Add(Primary("Simpan Lead", (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(school.Text))
            {
                _status.Text = "Nama sekolah/lembaga wajib diisi.";
                return;
            }
            decimal.TryParse(potential.Text.Replace(".", "").Replace(",", ""), out var value);
            _state.Leads.Add(new LeadRecord
            {
                School = school.Text.Trim(),
                Contact = contact.Text.Trim(),
                Stage = stage.SelectedItem?.ToString() ?? "Prospek",
                Potential = value
            });
            Persist("Lead tersimpan");
            ShowSales();
        }));

        panel.Children.Add(new TextBlock { Text = $"Pipeline • {_state.Leads.Count} lead", FontSize = 20, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold, Margin = new Thickness(0, 14, 0, 0) });
        foreach (var x in _state.Leads.ToList())
            panel.Children.Add(Card(x.School, $"{x.Contact} • {x.Stage} • Rp {x.Potential.ToString("N0", Id)}", () =>
            {
                _state.Leads.RemoveAll(y => y.Id == x.Id);
                Persist("Lead dihapus");
                ShowSales();
            }));
        SetPage("Sales & CRM", Scroll(panel));
    }

    private void ShowOperations()
    {
        var panel = PageStack();
        panel.Children.Add(new TextBlock { Text = "Tambah Aktivitas Operasional", FontSize = 20, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold });
        var date = Box("Tanggal", DateTime.Today.ToString("yyyy-MM-dd"));
        var activity = Box("Aktivitas", placeholder: "Briefing, rundown, vendor, keberangkatan...");
        var pic = Box("PIC");
        var status = Combo("Status", "Belum", "Proses", "Siap", "Selesai");
        panel.Children.Add(date); panel.Children.Add(activity); panel.Children.Add(pic); panel.Children.Add(status);
        panel.Children.Add(Primary("Simpan Aktivitas", (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(activity.Text))
            {
                _status.Text = "Aktivitas wajib diisi.";
                return;
            }
            _state.Operations.Add(new OperationRecord
            {
                Date = date.Text.Trim(),
                Activity = activity.Text.Trim(),
                PIC = pic.Text.Trim(),
                Status = status.SelectedItem?.ToString() ?? "Belum"
            });
            Persist("Aktivitas tersimpan");
            ShowOperations();
        }));
        panel.Children.Add(new TextBlock { Text = "Checklist Operasional", FontSize = 20, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold, Margin = new Thickness(0, 14, 0, 0) });
        foreach (var x in _state.Operations.OrderByDescending(x => x.Date).ToList())
            panel.Children.Add(Card($"{x.Date} • {x.Activity}", $"PIC: {x.PIC} • {x.Status}", () =>
            {
                _state.Operations.RemoveAll(y => y.Id == x.Id);
                Persist("Aktivitas dihapus");
                ShowOperations();
            }));
        SetPage("Operasional", Scroll(panel));
    }

    private void ShowFinance()
    {
        var panel = PageStack();
        panel.Children.Add(new TextBlock { Text = "Catat Transaksi", FontSize = 20, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold });
        var date = Box("Tanggal", DateTime.Today.ToString("yyyy-MM-dd"));
        var desc = Box("Keterangan");
        var type = Combo("Jenis", "Pemasukan", "Pengeluaran");
        var amount = Box("Nominal (Rp)", placeholder: "0");
        panel.Children.Add(date); panel.Children.Add(desc); panel.Children.Add(type); panel.Children.Add(amount);
        panel.Children.Add(Primary("Simpan Transaksi", (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(desc.Text))
            {
                _status.Text = "Keterangan wajib diisi.";
                return;
            }
            if (!decimal.TryParse(amount.Text.Replace(".", "").Replace(",", ""), out var value) || value <= 0)
            {
                _status.Text = "Nominal tidak valid.";
                return;
            }
            _state.Finances.Add(new FinanceRecord
            {
                Date = date.Text.Trim(),
                Description = desc.Text.Trim(),
                Type = type.SelectedItem?.ToString() ?? "Pemasukan",
                Amount = value
            });
            Persist("Transaksi tersimpan");
            ShowFinance();
        }));

        var income = _state.Finances.Where(x => x.Type == "Pemasukan").Sum(x => x.Amount);
        var expense = _state.Finances.Where(x => x.Type == "Pengeluaran").Sum(x => x.Amount);
        panel.Children.Add(Card("Ringkasan", $"Pemasukan Rp {income.ToString("N0", Id)} • Pengeluaran Rp {expense.ToString("N0", Id)} • Saldo Rp {(income - expense).ToString("N0", Id)}"));
        panel.Children.Add(new TextBlock { Text = "Riwayat Transaksi", FontSize = 20, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold, Margin = new Thickness(0, 14, 0, 0) });
        foreach (var x in _state.Finances.OrderByDescending(x => x.Date).ToList())
            panel.Children.Add(Card($"{x.Date} • {x.Description}", $"{x.Type} • Rp {x.Amount.ToString("N0", Id)}", () =>
            {
                _state.Finances.RemoveAll(y => y.Id == x.Id);
                Persist("Transaksi dihapus");
                ShowFinance();
            }));
        SetPage("Keuangan", Scroll(panel));
    }

    private void ShowHr()
    {
        var panel = PageStack();
        panel.Children.Add(new TextBlock { Text = "Tambah SDM", FontSize = 20, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold });
        var name = Box("Nama");
        var role = Box("Role", placeholder: "Sales, Manager, TL/MC, Ops...");
        var scheme = Box("Skema", placeholder: "Fixed, per kegiatan, komisi...");
        var status = Combo("Status", "Aktif", "Nonaktif");
        panel.Children.Add(name); panel.Children.Add(role); panel.Children.Add(scheme); panel.Children.Add(status);
        panel.Children.Add(Primary("Simpan SDM", (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(name.Text) || string.IsNullOrWhiteSpace(role.Text))
            {
                _status.Text = "Nama dan role wajib diisi.";
                return;
            }
            _state.Staff.Add(new StaffRecord
            {
                Name = name.Text.Trim(),
                Role = role.Text.Trim(),
                Scheme = scheme.Text.Trim(),
                Status = status.SelectedItem?.ToString() ?? "Aktif"
            });
            Persist("SDM tersimpan");
            ShowHr();
        }));
        panel.Children.Add(new TextBlock { Text = "Daftar SDM", FontSize = 20, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold, Margin = new Thickness(0, 14, 0, 0) });
        foreach (var x in _state.Staff.ToList())
            panel.Children.Add(Card(x.Name, $"{x.Role} • {x.Scheme} • {x.Status}", () =>
            {
                _state.Staff.RemoveAll(y => y.Id == x.Id);
                Persist("SDM dihapus");
                ShowHr();
            }));
        SetPage("SDM & Payroll", Scroll(panel));
    }

    private void ShowReports()
    {
        var panel = PageStack();
        var pax = _state.Bookings.Where(x => x.Status != "Batal").Sum(x => x.Participants);
        var income = _state.Finances.Where(x => x.Type == "Pemasukan").Sum(x => x.Amount);
        var expense = _state.Finances.Where(x => x.Type == "Pengeluaran").Sum(x => x.Amount);
        panel.Children.Add(Card("Pencapaian Peserta", $"{pax:N0} / {_state.TargetPax:N0} pax"));
        panel.Children.Add(Card("Booking", $"{_state.Bookings.Count:N0} transaksi booking"));
        panel.Children.Add(Card("Sales", $"{_state.Leads.Count:N0} lead aktif/tersimpan"));
        panel.Children.Add(Card("Operasional", $"{_state.Operations.Count:N0} aktivitas"));
        panel.Children.Add(Card("Keuangan", $"Pemasukan Rp {income.ToString("N0", Id)} • Pengeluaran Rp {expense.ToString("N0", Id)} • Saldo Rp {(income - expense).ToString("N0", Id)}"));
        panel.Children.Add(Card("SDM", $"{_state.Staff.Count:N0} profil"));
        SetPage("Laporan", Scroll(panel));
    }

    private void ShowMaster()
    {
        var panel = PageStack();
        panel.Children.Add(new TextBlock { Text = "Target Peserta Bulanan", FontSize = 20, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold });
        var target = Box("Target Pax", _state.TargetPax.ToString());
        panel.Children.Add(target);
        panel.Children.Add(Primary("Simpan Target", (_, _) =>
        {
            if (!int.TryParse(target.Text, out var n) || n < 1)
            {
                _status.Text = "Target pax tidak valid.";
                return;
            }
            _state.TargetPax = n;
            Persist("Target diperbarui");
            ShowMaster();
        }));
        panel.Children.Add(Card("Master Harga", "Harga/HPP produksi tidak diubah dari aplikasi lokal sampai API master ERP resmi terhubung."));
        panel.Children.Add(Card("Master Role", "Owner, Manager, Sales, Admin, Finance, Tour Leader, Operations."));
        SetPage("Master Data", Scroll(panel));
    }

    private void ShowSettings()
    {
        var panel = PageStack();
        panel.Children.Add(Card("Penyimpanan Lokal", _store.FilePath));
        var api = Environment.GetEnvironmentVariable("GMU_EDUTRANS_API_BASE");
        panel.Children.Add(Card("Backend ERP", string.IsNullOrWhiteSpace(api)
            ? "Belum dikonfigurasi. Aplikasi berjalan mandiri dengan penyimpanan lokal."
            : $"API base terkonfigurasi: {api}"));
        panel.Children.Add(Card("Mode Aplikasi", "WinUI 3 native • x64 • self-contained • startup smoke-tested"));
        SetPage("Pengaturan", Scroll(panel));
    }
}
