using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ColorModel : INotifyPropertyChanged
{
    private double _r, _g, _b;
    private double _c, _m, _y, _k;
    private double _x, _y1, _z;

    public double R
    {
        get => _r;
        set
        {
            if (_r != value)
            {
                _r = value;
                OnPropertyChanged(nameof(R));
                UpdateCMYK();
                UpdateXYZ();
            }
        }
    }

    public double G
    {
        get => _g;
        set
        {
            if (_g != value)
            {
                _g = value;
                OnPropertyChanged(nameof(G));
                UpdateCMYK();
                UpdateXYZ();
            }
        }
    }

    public double B
    {
        get => _b;
        set
        {
            if (_b != value)
            {
                _b = value;
                OnPropertyChanged(nameof(B));
                UpdateCMYK();
                UpdateXYZ();
            }
        }
    }

    public double C
    {
        get => _c;
        set
        {
            if (_c != value)
            {
                _c = value;
                OnPropertyChanged(nameof(C));
                UpdateRGB();
                UpdateXYZ();
            }
        }
    }

    public double M
    {
        get => _m;
        set
        {
            if (_m != value)
            {
                _m = value;
                OnPropertyChanged(nameof(M));
                UpdateRGB();
                UpdateXYZ();
            }
        }
    }

    public double Y
    {
        get => _y;
        set
        {
            if (_y != value)
            {
                _y = value;
                OnPropertyChanged(nameof(Y));
                UpdateRGB();
                UpdateXYZ();
            }
        }
    }

    public double K
    {
        get => _k;
        set
        {
            if (_k != value)
            {
                _k = value;
                OnPropertyChanged(nameof(K));
                UpdateRGB();
                UpdateXYZ();
            }
        }
    }

    public double X
    {
        get => _x;
        set
        {
            if (_x != value)
            {
                _x = value;
                OnPropertyChanged(nameof(X));
                UpdateRGBFromXYZ();
                UpdateCMYK();
            }
        }
    }

    public double Y1
    {
        get => _y1;
        set
        {
            if (_y1 != value)
            {
                _y1 = value;
                OnPropertyChanged(nameof(Y1));
                UpdateRGBFromXYZ();
                UpdateCMYK();
            }
        }
    }

    public double Z
    {
        get => _z;
        set
        {
            if (_z != value)
            {
                _z = value;
                OnPropertyChanged(nameof(Z));
                UpdateRGBFromXYZ();
                UpdateCMYK();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private void UpdateCMYK()
    {
        double r = R / 255.0;
        double g = G / 255.0;
        double b = B / 255.0;

        double k = 1 - Math.Max(r, Math.Max(g, b));
        double c = (1 - r - k) / (1 - k);
        double m = (1 - g - k) / (1 - k);
        double y = (1 - b - k) / (1 - k);

        _c = double.IsNaN(c) ? 0 : Math.Min(Math.Max(Math.Round(c * 100) / 100.0, 0), 1);
        _m = double.IsNaN(m) ? 0 : Math.Min(Math.Max(Math.Round(m * 100) / 100.0, 0), 1);
        _y = double.IsNaN(y) ? 0 : Math.Min(Math.Max(Math.Round(y * 100) / 100.0, 0), 1);
        _k = double.IsNaN(k) ? 0 : Math.Min(Math.Max(Math.Round(k * 100) / 100.0, 0), 1);

        OnPropertyChanged(nameof(C));
        OnPropertyChanged(nameof(M));
        OnPropertyChanged(nameof(Y));
        OnPropertyChanged(nameof(K));
    }

    private void UpdateXYZ()
    {
        double r = R / 255.0;
        double g = G / 255.0;
        double b = B / 255.0;

        _x = Math.Min(Math.Max(Math.Round((r * 0.4124 + g * 0.3576 + b * 0.1805) * 100), 0), 100);
        _y1 = Math.Min(Math.Max(Math.Round((r * 0.2126 + g * 0.7152 + b * 0.0722) * 100), 0), 100);
        _z = Math.Min(Math.Max(Math.Round((r * 0.0193 + g * 0.1192 + b * 0.9505) * 100), 0), 100);

        OnPropertyChanged(nameof(X));
        OnPropertyChanged(nameof(Y1));
        OnPropertyChanged(nameof(Z));
    }

    private void UpdateRGB()
    {
        _r = Math.Min(Math.Max(Math.Round(255 * (1 - C) * (1 - K)), 0), 255);
        _g = Math.Min(Math.Max(Math.Round(255 * (1 - M) * (1 - K)), 0), 255);
        _b = Math.Min(Math.Max(Math.Round(255 * (1 - Y) * (1 - K)), 0), 255);

        OnPropertyChanged(nameof(R));
        OnPropertyChanged(nameof(G));
        OnPropertyChanged(nameof(B));
    }



    private void UpdateRGBFromXYZ()
    {
        double x = X / 100.0;
        double y = Y1 / 100.0;
        double z = Z / 100.0;

        double r = x * 3.2406 + y * -1.5372 + z * -0.4986;
        double g = x * -0.9689 + y * 1.8758 + z * 0.0415;
        double b = x * 0.0557 + y * -0.2040 + z * 1.0570;

        r = r > 0.0031308 ? 1.055 * Math.Pow(r, 1 / 2.4) - 0.055 : 12.92 * r;
        g = g > 0.0031308 ? 1.055 * Math.Pow(g, 1 / 2.4) - 0.055 : 12.92 * g;
        b = b > 0.0031308 ? 1.055 * Math.Pow(b, 1 / 2.4) - 0.055 : 12.92 * b;

        _r = Math.Round(Math.Max(0, Math.Min(1, r)) * 255);
        _g = Math.Round(Math.Max(0, Math.Min(1, g)) * 255);
        _b = Math.Round(Math.Max(0, Math.Min(1, b)) * 255);

        OnPropertyChanged(nameof(R));
        OnPropertyChanged(nameof(G));
        OnPropertyChanged(nameof(B));
    }


}

