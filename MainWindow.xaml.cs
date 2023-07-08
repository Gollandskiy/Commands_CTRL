using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Commands_CTRL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new RichTextBoxViewModel();
            InitializeHotkeys();
        }

        private void InitializeHotkeys()
        {
            var hotkeys = new Key[] { Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9 };
            var commands = new ICommand[]
            {
                ((RichTextBoxViewModel)DataContext).BoldCommand,
                ((RichTextBoxViewModel)DataContext).ItalicCommand,
                ((RichTextBoxViewModel)DataContext).UnderlineCommand,
                ((RichTextBoxViewModel)DataContext).ClearCommand,
                ((RichTextBoxViewModel)DataContext).Font15Command,
                ((RichTextBoxViewModel)DataContext).Font30Command,
                ((RichTextBoxViewModel)DataContext).RedColorCommand,
                ((RichTextBoxViewModel)DataContext).GreenColorCommand,
                ((RichTextBoxViewModel)DataContext).BlueColorCommand
            };

            for (int i = 0; i < hotkeys.Length; i++)
            {
                var hotkey = new Hotkey(hotkeys[i], ModifierKeys.Control);
                hotkey.HotkeyPressed += (sender, e) => ExecuteCommand(commands[i]);
                hotkey.Register(this);
            }
        }

        private void ExecuteCommand(ICommand command)
        {
            var target = CommandHelpers.GetCommandTarget(richTextBox);
            if (command.CanExecute(target))
            {
                command.Execute(target);
            }
        }
    }

    public class RichTextBoxViewModel : INotifyPropertyChanged
    {
        private ICommand _boldCommand;
        private ICommand _italicCommand;
        private ICommand _underlineCommand;
        private ICommand _clearCommand;
        private ICommand _font15Command;
        private ICommand _font30Command;
        private ICommand _redColorCommand;
        private ICommand _greenColorCommand;
        private ICommand _blueColorCommand;

        private void ToggleUnderline(TextSelection selection)
        {
            var textDecoration = selection.GetPropertyValue(Inline.TextDecorationsProperty);
            var textDecorations = textDecoration as TextDecorationCollection;

            if (textDecorations != null && textDecorations.Count > 0)
            {
                selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
            else
            {
                selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }
        private void ClearFormatting(TextSelection selection)
        {
            selection.ClearAllProperties();
        }
        private void SetFontSize(TextSelection selection, double fontSize)
        {
            selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
        }
        private void SetTextColor(TextSelection selection, Color color)
        {

            selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
        }

        public ICommand BoldCommand
        {
            get { return _boldCommand ?? (_boldCommand = new RelayCommand<TextSelection>(BoldExecute)); }
        }

        public ICommand ItalicCommand
        {
            get { return _italicCommand ?? (_italicCommand = new RelayCommand<TextSelection>(ItalicExecute)); }
        }

        public ICommand UnderlineCommand
        {
            get { return _underlineCommand ?? (_underlineCommand = new RelayCommand<TextSelection>(UnderlineExecute)); }
        }

        public ICommand ClearCommand
        {
            get { return _clearCommand ?? (_clearCommand = new RelayCommand<TextSelection>(ClearExecute)); }
        }

        public ICommand Font15Command
        {
            get { return _font15Command ?? (_font15Command = new RelayCommand<TextSelection>(Font15Execute)); }
        }

        public ICommand Font30Command
        {
            get { return _font30Command ?? (_font30Command = new RelayCommand<TextSelection>(Font30Execute)); }
        }

        public ICommand RedColorCommand
        {
            get { return _redColorCommand ?? (_redColorCommand = new RelayCommand<TextSelection>(RedColorExecute)); }
        }

        public ICommand GreenColorCommand
        {
            get { return _greenColorCommand ?? (_greenColorCommand = new RelayCommand<TextSelection>(GreenColorExecute)); }
        }

        public ICommand BlueColorCommand
        {
            get { return _blueColorCommand ?? (_blueColorCommand = new RelayCommand<TextSelection>(BlueColorExecute)); }
        }

        private void BoldExecute(TextSelection selection)
        {
            ToggleBold(selection);
        }

        private void ItalicExecute(TextSelection selection)
        {
            ToggleItalic(selection);
        }

        private void UnderlineExecute(TextSelection selection)
        {
            ToggleUnderline((System.Windows.Documents.TextSelection)selection);
        }

        private void ClearExecute(TextSelection selection)
        {
            ClearFormatting(selection);
        }

        private void Font15Execute(TextSelection selection)
        {
            SetFontSize(selection, 15);
        }

        private void Font30Execute(TextSelection selection)
        {
            SetFontSize(selection, 30);
        }

        private void RedColorExecute(TextSelection selection)
        {
            SetTextColor(selection, Colors.Red);
        }

        private void GreenColorExecute(TextSelection selection)
        {
            SetTextColor(selection, Colors.Green);
        }

        private void BlueColorExecute(TextSelection selection)
        {
            SetTextColor(selection, Colors.Blue);
        }

        private void ToggleBold(TextSelection selection)
        {
            var fontWeight = selection.GetPropertyValue(TextElement.FontWeightProperty);
            if (fontWeight != DependencyProperty.UnsetValue && fontWeight.Equals(FontWeights.Bold))
                selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            else
                selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
        }

        private void ToggleItalic(TextSelection selection)
        {
            var fontStyle = selection.GetPropertyValue(TextElement.FontStyleProperty);
            if (fontStyle != DependencyProperty.UnsetValue && fontStyle.Equals(FontStyles.Italic))
                selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            else
                selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
        }


        private void ToggleUnderline2(TextSelection selection)
            {
                var textDecoration = selection.GetPropertyValue(Inline.TextDecorationsProperty);
                if (textDecoration != DependencyProperty.UnsetValue && textDecoration.Equals(TextDecorations.Underline))
                    selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                else
                    selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }

            private void ClearFormatting2(TextSelection selection)
            {
                selection.ClearAllProperties();
            }

            private void SetFontSize2(TextSelection selection, double fontSize)
            {
                selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
            }

            private void SetTextColor2(TextSelection selection, Color color)
            {
                selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
            }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private Action<T> _execute;
        private Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    public static class CommandHelpers
    {
        public static object GetCommandTarget(DependencyObject obj)
        {
            return obj is TextBox textBox ? textBox.SelectionBrush : null;
        }
        public static TextSelection GetTextSelection(RichTextBox richTextBox)
        {
            return richTextBox.Selection;
        }
    }

    public class Hotkey
    {
        public Key Key { get; set; }
        public ModifierKeys Modifier { get; set; }

        public event EventHandler HotkeyPressed;

        public Hotkey(Key key, ModifierKeys modifier)
        {
            Key = key;
            Modifier = modifier;
        }

        public void Register(Window window)
        {
            window.PreviewKeyDown += Window_PreviewKeyDown;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key && Keyboard.Modifiers == Modifier)
            {
                HotkeyPressed?.Invoke(this, EventArgs.Empty);
                e.Handled = true;
            }
        }
    }

    public class CommandBindingsConverter : System.Windows.Markup.MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new System.Windows.Input.CommandBindingCollection();
        }
    }
}