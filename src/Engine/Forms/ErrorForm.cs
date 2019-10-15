using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommandLine;
using CommandLine.Text;

namespace BouncyBox.VorpalEngine.Engine.Forms
{
    /// <summary>
    ///     A window used to display fatal error messages.
    /// </summary>
    public sealed class ErrorForm : Form
    {
        private ErrorForm(string windowCaption, string label, string message)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            ClientSize = new Size(800, 500);
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Text = windowCaption;

            const int margin = 10;
            var messageLabel =
                new Label
                {
                    AutoSize = true,
                    Location = new Point(10, 10),
                    TabIndex = 0,
                    Text = label
                };
            var exitButton =
                new Button
                {
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                    Size = new Size(100, 30),
                    TabIndex = 3,
                    Text = "E&xit"
                };
            var copyButton =
                new Button
                {
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                    Size = new Size(100, 30),
                    TabIndex = 2,
                    Text = "&Copy"
                };

            exitButton.Location = new Point(ClientSize.Width - exitButton.Width - margin, ClientSize.Height - exitButton.Height - margin);
            copyButton.Location = new Point(exitButton.Left - copyButton.Width - margin, exitButton.Top);

            var messageTextBox =
                new TextBox
                {
                    Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                    Bounds = Rectangle.FromLTRB(messageLabel.Left, messageLabel.Bottom + 5, ClientSize.Width - margin, exitButton.Top - margin),
                    Font = new Font(new FontFamily("Consolas"), DefaultFont.Size, DefaultFont.Style, DefaultFont.Unit),
                    Multiline = true,
                    ReadOnly = true,
                    ScrollBars = ScrollBars.Both,
                    TabIndex = 1,
                    Text = message,
                    WordWrap = false
                };

            Controls.AddRange(new Control[] { messageLabel, messageTextBox, copyButton, exitButton });

            exitButton.Click += (sender, args) => Close();
            copyButton.Click += (sender, args) => Clipboard.SetText(messageTextBox.Text);

            exitButton.Select();
        }

        /// <summary>
        ///     Creates a window that will display a message about invalid command line arguments.
        /// </summary>
        /// <param name="parserResult">The result of command line parameter parsing.</param>
        /// <param name="windowCaption">A window caption.</param>
        /// <returns>Returns the error window.</returns>
        /// <exception cref="ArgumentException">
        ///     Thrown when <paramref name="parserResult" /> indicates the command line parameters were
        ///     successfully parsed.
        /// </exception>
        public static ErrorForm CreateForInvalidCommandLineArguments<TOptions>(ParserResult<TOptions> parserResult, string windowCaption = "Vorpal Engine")
        {
            if (parserResult.Tag == ParserResultType.Parsed)
            {
                throw new ArgumentException(nameof(parserResult));
            }

            ErrorForm? errorForm = null;
            var helpText = new HelpText { Heading = "Options:" };

            helpText.AddOptions(parserResult);

            parserResult.WithNotParsed(
                errors =>
                {
                    SentenceBuilder sentenceBuilder = SentenceBuilder.Create();
                    var stringBuilder = new StringBuilder();

                    stringBuilder
                        .AppendJoin(Environment.NewLine, errors.Select(sentenceBuilder.FormatError))
                        .AppendLine()
                        .AppendLine()
                        .AppendLine(helpText);

                    errorForm = new ErrorForm(windowCaption, "Invalid command line arguments. &Details:", stringBuilder.ToString());
                });

            Debug.Assert(errorForm != null);

            return errorForm;
        }

        /// <summary>
        ///     Creates a window that will display a message about an unhandled exception.
        /// </summary>
        /// <param name="exception">The unhandled exception.</param>
        /// <param name="windowCaption">A window caption.</param>
        /// <returns>Returns the error window.</returns>
        public static ErrorForm CreateForUnhandledException(Exception exception, string windowCaption = "Vorpal Engine")
        {
            return new ErrorForm(windowCaption, "An unhandled exception has occurred. &Details:", exception.ToString());
        }

        /// <inheritdoc />
        protected override void OnLoad(EventArgs e)
        {
            MinimumSize = Size;

            base.OnLoad(e);
        }
    }
}