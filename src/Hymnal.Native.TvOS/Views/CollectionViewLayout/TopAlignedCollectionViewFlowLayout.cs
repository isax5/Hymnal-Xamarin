using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using UIKit;

namespace Hymnal.Native.TvOS.Views.CollectionViewLayout
{
    public class TopAlignedCollectionViewFlowLayout : UICollectionViewFlowLayout
    {
        public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect(CGRect rect)
        {
            //return base.LayoutAttributesForElementsInRect(rect);
            if (base.LayoutAttributesForElementsInRect(rect) != null)
            {
                UICollectionViewLayoutAttributes[] attrs = base.LayoutAttributesForElementsInRect(rect);

                nfloat baseline = -2;
                var sameLineElements = new List<UICollectionViewLayoutAttributes>();

                foreach (UICollectionViewLayoutAttributes element in attrs)
                {
                    if (element.RepresentedElementCategory == UICollectionElementCategory.Cell)
                    {
                        var frame = element.Frame;
                        var centerY = frame.GetMidY();

                        if (Math.Abs(centerY - baseline) > 1)
                        {
                            baseline = centerY;
                            //TopAlignedCollectionViewFlowLayout.AlignToTopForSameLineElements(sameLineElements);
                            AlignToTopForSameLineElements(sameLineElements);
                            sameLineElements.Clear();
                        }
                        sameLineElements.Add(element);
                    }
                }
                //TopAlignedCollectionViewFlowLayout.AlignToTopForSameLineElements(sameLineElements) // align one more time for the last line
                AlignToTopForSameLineElements(sameLineElements);
                return attrs;
            }
            return null;
        }

        private static void AlignToTopForSameLineElements(IEnumerable<UICollectionViewLayoutAttributes> cells)
        {
            // If only 1 cell in the row its already top aligned.
            if (cells.Count() <= 1)
                return;

            // The tallest cell has the correct Y value for all the other cells in the row
            var tallestCell = cells.OrderByDescending(cell => cell.Frame.Height).First();

            var topOfRow = tallestCell.Frame.Y;

            foreach (var cell in cells)
            {
                if (cell.Frame.Y == topOfRow)
                    continue;

                var frame = cell.Frame;

                frame.Y = topOfRow;

                cell.Frame = frame;
            }
        }
    }
}