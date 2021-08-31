namespace Hymnal.XF.Behaviors.FastEntry
{
    public sealed class MaxLength
    {
        public string ProcessLength(string entryText, string oldString, string newString, int? maxLength)
        {
            var output = entryText;

            if (oldString != null)
            {
                if (newString.Length > oldString.Length)
                {
                    // if Entry text is longer then valid length
                    if (entryText.Length > maxLength)
                    {
                        entryText = entryText.Remove(entryText.Length - 1); // remove last char

                        output = entryText;
                    }
                }
            }

            return output;
        }
    }
}
