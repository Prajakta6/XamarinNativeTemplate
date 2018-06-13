using System;
using Android.Graphics;
using Android.Text.Style;

namespace XamarinNativeTemplate.Droid
{
    public class CustomTypefaceSpan : TypefaceSpan
    {
        Typeface newTypeFace;

        public CustomTypefaceSpan(string family, Typeface pTypeFace) : base(family)
        {
            newTypeFace = pTypeFace;
        }

        public override void UpdateDrawState(Android.Text.TextPaint ds)
        {
            ApplyCustomTypeFace(ds, newTypeFace);
        }

        public override void UpdateMeasureState(Android.Text.TextPaint paint)
        {
            ApplyCustomTypeFace(paint, newTypeFace);
        }

        static void ApplyCustomTypeFace(Paint paint, Typeface tf)
        {
            TypefaceStyle oldTFStyle;

            Typeface oldTypeFace = paint.Typeface;

            if (oldTypeFace == null)
            {
                oldTFStyle = 0;
            }
            else
            {
                oldTFStyle = oldTypeFace.Style;
            }

            var fake = oldTFStyle & ~tf.Style;

            if (fake == TypefaceStyle.Bold)
            {
                paint.FakeBoldText = true;
            }

            if (fake == TypefaceStyle.Italic)
            {
                paint.TextSkewX = -0.25f;
            }

            paint.SetTypeface(tf);
        }
    }
}