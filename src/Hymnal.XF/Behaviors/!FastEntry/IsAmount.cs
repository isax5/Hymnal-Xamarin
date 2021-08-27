namespace Hymnal.XF.Behaviors.FastEntry
{
    public sealed class IsAmount
    {
        public string ProcessIsAmount(string entryText, string oldString, string newString)
        {
            var output = entryText;

            if (oldString != null)
            {
                if (newString.Length > oldString.Length)
                {
                    var hil = oldString.Length > 0 ? entryText.Remove(0, entryText.Length - 1) : newString;

                    var isNum = double.TryParse(hil, out _);
                    if (!isNum && hil != ".")
                    {
                        entryText = entryText.Remove(entryText.Length - 1); // remove last char
                        output = entryText;
                    }
                    else if (hil == ".")
                    {
                        if (oldString.Contains("."))
                        {
                            entryText = entryText.Remove(entryText.Length - 1); // remove last char
                            output = entryText;
                        }
                    }
                }
                else
                {
                    var hil = entryText.Substring(entryText.Length - 1, 1);
                    var isNum = double.TryParse(hil, out _);

                    if (!isNum && hil != ".")
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
