namespace NetClient.Rest
{
    /// <summary>
    ///     Represents the HTTP URI route for a REST resource.
    /// </summary>
    public class Route
    {
        /// <summary>
        ///     Gets or sets the nodes.
        /// </summary>
        /// <value>The nodes.</value>
        public string[] Nodes { get; set; }

        /// <summary>
        ///     Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public string[] Parameters { get; set; }

        /// <summary>
        ///     Gets or sets the templates.
        /// </summary>
        /// <value>The templates.</value>
        public string[] Templates { get; set; }
    }
}