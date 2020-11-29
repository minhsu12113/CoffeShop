using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CoffeShop.Utility
{
    /// <summary>
    /// Class that provides the TextBox attached property
    /// </summary>
    public static class TextBoxService
    {
        /// <summary>
        /// TextBox Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty IsNumericOnlyProperty = DependencyProperty.RegisterAttached(
           "IsNumericOnly",
           typeof(bool),
           typeof(TextBoxService),
           new UIPropertyMetadata(false, OnIsNumericOnlyChanged));

        /// <summary>
        /// Gets the IsNumericOnly property.  This dependency property indicates the text box only allows numeric or not.
        /// </summary>
        /// <param name="d"><see cref="DependencyObject"/> to get the property from</param>
        /// <returns>The value of the StatusBarContent property</returns>
        public static bool GetIsNumericOnly(DependencyObject d)
        {
            return (bool)d.GetValue(IsNumericOnlyProperty);
        }

        /// <summary>
        /// Sets the IsNumericOnly property.  This dependency property indicates the text box only allows numeric or not.
        /// </summary>
        /// <param name="d"><see cref="DependencyObject"/> to set the property on</param>
        /// <param name="value">value of the property</param>
        public static void SetIsNumericOnly(DependencyObject d, bool value)
        {
            d.SetValue(IsNumericOnlyProperty, value);
        }

        /// <summary>
        /// Handles changes to the IsNumericOnly property.
        /// </summary>
        /// <param name="d"><see cref="DependencyObject"/> that fired the event</param>
        /// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
        private static void OnIsNumericOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool isNumericOnly = (bool)e.NewValue;

            TextBox textBox = (TextBox)d;

            if (isNumericOnly)
            {
                textBox.PreviewTextInput += BlockNonDigitCharacters;
                textBox.PreviewKeyDown += ReviewKeyDown;
            }
            else
            {
                textBox.PreviewTextInput -= BlockNonDigitCharacters;
                textBox.PreviewKeyDown -= ReviewKeyDown;
            }
        }

        /// <summary>
        /// Disallows non-digit character.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="TextCompositionEventArgs"/> that contains the event data.</param>
        private static void BlockNonDigitCharacters(object sender, TextCompositionEventArgs e)
        {
            foreach (char ch in e.Text)
            {
                if (!Char.IsDigit(ch))
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Disallows a space key.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="KeyEventArgs"/> that contains the event data.</param>
        private static void ReviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                // Disallow the space key, which doesn't raise a PreviewTextInput event.
                e.Handled = true;
            }
        }


        /// <summary>
        /// MinValue Dependency Property
        /// </summary>
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.RegisterAttached(
           "MinValue",
           typeof(int),
           typeof(TextBoxService),
           new UIPropertyMetadata(0, OnMinValueChanged));

        public static int GetMinValue(DependencyObject d)
        {
            return (int)d.GetValue(MinValueProperty);
        }

        public static void SetMinValue(DependencyObject d, int value)
        {
            d.SetValue(MinValueProperty, value);
        }

        /// <summary>
        /// MaxValue Dependency Property
        /// </summary>
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.RegisterAttached(
           "MaxValue",
           typeof(int),
           typeof(TextBoxService),
           new UIPropertyMetadata(0, OnMaxValueChanged));

        public static int GetMaxValue(DependencyObject d)
        {
            return (int)d.GetValue(MaxValueProperty);
        }

        public static void SetMaxValue(DependencyObject d, int value)
        {
            d.SetValue(MaxValueProperty, value);
        }

        private static void OnMinValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = (TextBox)d;
            textBox.LostFocus += (s, arg) =>
            {
                TextBox txt = null;
                try
                {
                    txt = (TextBox)s;
                    int curValue = Convert.ToInt32(txt.Text);
                    if (curValue < GetMinValue(txt))
                    {
                        txt.Text = GetMinValue(txt).ToString();
                    }
                }
                catch (System.Exception)
                {
                    txt.Text = GetMaxValue(txt).ToString();
                }
            };  
        }

        private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox txt = null;
            TextBox textBox = (TextBox)d;
            textBox.LostFocus += (s, arg) =>
            {
                try
                {
                    txt = (TextBox)s;
                    int curValue = Convert.ToInt32(txt.Text);
                    if (curValue > GetMaxValue(txt))
                    {
                        txt.Text = GetMaxValue(txt).ToString();
                    }
                }
                catch (System.Exception)
                {
                    txt.Text = GetMaxValue(txt).ToString();
                }
            };
        }

        //IsFloatNumericOnly - Begin
        /// <summary>
        /// TextBox Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty IsFloatNumericOnlyProperty = DependencyProperty.RegisterAttached(
           "IsFloatNumericOnly",
           typeof(bool),
           typeof(TextBoxService),
           new UIPropertyMetadata(false, OnIsFloatNumericOnlyChanged));

        /// <summary>
        /// Gets the IsFloatNumericOnly property.  This dependency property indicates the text box only allows numeric or not.
        /// </summary>
        /// <param name="d"><see cref="DependencyObject"/> to get the property from</param>
        /// <returns>The value of the StatusBarContent property</returns>
        public static bool GetIsFloatNumericOnly(DependencyObject d)
        {
            return (bool)d.GetValue(IsFloatNumericOnlyProperty);
        }

        /// <summary>
        /// Sets the IsFloatNumericOnly property.  This dependency property indicates the text box only allows numeric or not.
        /// </summary>
        /// <param name="d"><see cref="DependencyObject"/> to set the property on</param>
        /// <param name="value">value of the property</param>
        public static void SetIsFloatNumericOnly(DependencyObject d, bool value)
        {
            d.SetValue(IsFloatNumericOnlyProperty, value);
        }

        /// <summary>
        /// Handles changes to the IsFloatNumericOnly property.
        /// </summary>
        /// <param name="d"><see cref="DependencyObject"/> that fired the event</param>
        /// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
        private static void OnIsFloatNumericOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool isFloatNumericOnly = (bool)e.NewValue;

            TextBox textBox = (TextBox)d;

            if (isFloatNumericOnly)
            {
                textBox.PreviewTextInput += BlockNonFloatDigitCharacters;
                textBox.PreviewKeyDown += ReviewFloatKeyDown;
            }
            else
            {
                textBox.PreviewTextInput -= BlockNonFloatDigitCharacters;
                textBox.PreviewKeyDown -= ReviewFloatKeyDown;
            }
        }

        /// <summary>
        /// Disallows non-digit character.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="TextCompositionEventArgs"/> that contains the event data.</param>
        private static void BlockNonFloatDigitCharacters(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            foreach (char ch in e.Text)
            {
                if ((Char.IsDigit(ch) || ch == '.') == false)
                {
                    e.Handled = true;
                }
                else if (ch == '.')
                {
                    if (textBox.Text.Contains('.'))
                        e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Disallows a space key.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="KeyEventArgs"/> that contains the event data.</param>
        private static void ReviewFloatKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                // Disallow the space key, which doesn't raise a PreviewTextInput event.
                e.Handled = true;
            }
        }


        /// <summary>
        /// FloatMinValue Dependency Property
        /// </summary>
        public static readonly DependencyProperty FloatMinValueProperty = DependencyProperty.RegisterAttached(
           "FloatMinValue",
           typeof(double),
           typeof(TextBoxService),
           new UIPropertyMetadata((double)0, OnFloatMinValueChanged));

        public static double GetFloatMinValue(DependencyObject d)
        {
            return (double)d.GetValue(FloatMinValueProperty);
        }

        public static void SetFloatMinValue(DependencyObject d, double value)
        {
            d.SetValue(FloatMinValueProperty, value);
        }

        /// <summary>
        /// FloatMaxValue Dependency Property
        /// </summary>
        public static readonly DependencyProperty FloatMaxValueProperty = DependencyProperty.RegisterAttached(
           "FloatMaxValue",
           typeof(double),
           typeof(TextBoxService),
           new UIPropertyMetadata((double)0, OnFloatMaxValueChanged));

        public static double GetFloatMaxValue(DependencyObject d)
        {
            return (double)d.GetValue(FloatMaxValueProperty);
        }

        public static void SetFloatMaxValue(DependencyObject d, double value)
        {
            d.SetValue(FloatMaxValueProperty, value);
        }

        private static void OnFloatMinValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = (TextBox)d;
            textBox.LostFocus += (s, arg) =>
            {
                TextBox txt = null;
                try
                {
                    txt = (TextBox)s;
                    double curValue = Convert.ToDouble(txt.Text);
                    if (curValue < GetFloatMinValue(txt))
                    {
                        txt.Text = GetFloatMinValue(txt).ToString();
                    }
                }
                catch (System.Exception)
                {
                    txt.Text = GetFloatMaxValue(txt).ToString();
                }
            };
        }

        private static void OnFloatMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox txt = null;
            TextBox textBox = (TextBox)d;
            textBox.LostFocus += (s, arg) =>
            {
                try
                {
                    txt = (TextBox)s;
                    double curValue = Convert.ToDouble(txt.Text);
                    if (curValue > GetFloatMaxValue(txt))
                    {
                        txt.Text = GetFloatMaxValue(txt).ToString();
                    }
                }
                catch (System.Exception)
                {
                    txt.Text = GetFloatMaxValue(txt).ToString();
                }
            };
        }
        //IsFloatNumericOnly - End
    }
}
