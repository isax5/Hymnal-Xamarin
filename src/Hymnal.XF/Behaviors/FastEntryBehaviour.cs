using Hymnal.XF.Behaviors.FastEntry;
using Xamarin.Forms;

namespace Hymnal.XF.Behaviors
{
    public class FastEntryBehaviour : Behavior<Entry>
    {
        public int? MaxLength { get; set; }
        public string Mask { get; set; }
        public bool? IsNumeric { get; set; }
        public bool? IsAmount { get; set; }
        public bool? IsNumericWithSpace { get; set; }

        private readonly MaxLength xamarinMaxLength;
        private readonly IsNumeric xamarinIsNumeric;
        private readonly IsAmount xamarinIsAmount;
        private readonly Mask xamarinMask;
        private readonly IsNumericWithSpace xamarinIsNumericWithSpace;

        public FastEntryBehaviour()
        {
            xamarinMaxLength = new MaxLength();
            xamarinIsNumeric = new IsNumeric();
            xamarinIsAmount = new IsAmount();
            xamarinMask = new Mask();
            xamarinIsNumericWithSpace = new IsNumericWithSpace();
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = (Entry)sender;
            var oldString = args.OldTextValue;
            var newString = args.NewTextValue;
            var entryText = entry.Text;

            if (MaxLength != null && MaxLength >= 0 && entryText.Length > 0)
            {
                var output = xamarinMaxLength.ProcessLength(entryText, oldString, newString, MaxLength);
                if (output != entryText)
                {
                    entryText = output;
                    entry.Text = entryText;
                    return;
                }
            }

            if (IsNumeric != null && IsNumeric == true && entryText.Length > 0)
            {
                var output = xamarinIsNumeric.ProcessIsNumeric(entryText, oldString, newString);
                if (output != entryText)
                {
                    entryText = output;
                    entry.Text = entryText;
                    return;
                }
            }

            if (IsAmount != null && IsAmount == true && entryText.Length > 0)
            {
                var output = xamarinIsAmount.ProcessIsAmount(entryText, oldString, newString);
                if (output != entryText)
                {
                    entryText = output;
                    entry.Text = entryText;
                    return;
                }
            }

            if (IsNumericWithSpace != null && IsNumericWithSpace == true && entryText.Length > 0)
            {
                var output = xamarinIsNumericWithSpace.ProcessIsNumericWithSpace(entryText, oldString, newString);
                if (output != entryText)
                {
                    entryText = output;
                    entry.Text = entryText;
                    return;
                }
            }

            if (Mask != null && Mask.Length > 0 && entryText.Length > 0)
            {
                var output = xamarinMask.ProcessMask(entryText, oldString, newString, Mask);
                if (output != entryText)
                {
                    entryText = output;
                    entry.Text = entryText;
                    return;
                }
            }
            entry.Text = entryText;
        }
    }
}
