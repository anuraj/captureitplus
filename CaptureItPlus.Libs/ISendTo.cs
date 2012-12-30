namespace CaptureItPlus.Libs
{
    using System.Windows.Forms;
    using System;

    /// <summary>
    /// Send To Plugin Interface for CaptureIt Plus.
    /// </summary>
    public interface ISendTo : IDisposable
    {
        /// <summary>
        /// Gets unique identifier for the Plugin.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the Menu Text for the specified Plugin.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Executes the Plugin code.
        /// </summary>
        /// <param name="filename">The captured file name.</param>
        void Execute(string filename);

        ISendToHost Host { get; set; }
    }
}
