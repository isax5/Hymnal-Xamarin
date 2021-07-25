namespace Hymnal.XF.Behaviors.FastEntry
{
    public class Mask
    {
        public string ProcessMask(string entryText, string oldString, string newString, string mask)
        {
            var output = entryText;

            if (oldString != null)
            {
                if (returnCheck(oldString, mask))
                {
                    if (newString.Length > oldString.Length)
                    {
                        if (mask.Length >= newString.Length)
                        {
                            var ln = entryText.Length - 1;
                            var st = mask.Substring(ln, 1);

                            var newstr = oldString.Length > 0 ? newString.Substring(newString.Length - 1) : newString;
                            output = output.Length > 1 ? output.Remove(output.Length - 1, 1) : "";

                            if (st == "#")
                            {
                                output += newstr;
                            }
                            else
                            {
                                foreach (var s in mask.Substring(ln))
                                {
                                    if (s == '#')
                                    {
                                        output = output + newstr;
                                        break;
                                    }
                                    else
                                    {
                                        output = output + s;
                                    }
                                }
                            }
                        }
                        else
                        {
                            output = oldString;
                        }
                    }
                }
            }

            return output;
        }


        private bool returnCheck(string oldValue, string mask)
        {

            var i = 0;
            if (mask.Length < oldValue.Length)
            {
                return false;
            }

            foreach (var s in mask.Substring(0, oldValue.Length))
            {
                if (s.ToString() != "#" && s.ToString() != oldValue.Substring(i, 1))
                {
                    return false;
                }
                i++;
            }
            return true;
        }

    }
}
