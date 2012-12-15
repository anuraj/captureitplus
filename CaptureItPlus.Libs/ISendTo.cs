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

        /// <summary>
        /// Description about the plugin
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Help information about the plugin.
        /// </summary>
        string Help { get; }

        /// <summary>
        /// Gets whether the Send to execution is finished or not.
        /// </summary>
        bool IsFinished { get; }

        /// <summary>
        /// Gets the Plugin shortcut key.
        /// </summary>
        /// <remarks>If no key required, return Keys.None, doesn't throw NotImplementedException.</remarks>
        Keys ShortcutKey { get; }
    }
}
