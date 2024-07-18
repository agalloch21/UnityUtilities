using System;
using UnityEngine;

/*
 * 
 * the c# vvvv math library
 * 
 * definitions:
 * structs contain only constructors, data fields, properties, indexer and operators
 * higher dimensional datatypes contain operators for lower dimensional ones
 * all functions of the structs are static methods of the VMath class
 * 
 */



/// <summary>
/// VVVV Math Utilities 
/// </summary>
namespace VVVV.Utils.VMath
{
    using Vector2D = UnityEngine.Vector2;
    using Vector3D = UnityEngine.Vector3;
    using Vector4D = UnityEngine.Vector4;

    #region enums

    /// <summary>
    /// vvvv like modi for the Map function
    /// </summary>
    public enum TMapMode
    {
        /// <summary>
        /// Maps the value continously
        /// </summary>
        Float,
        /// <summary>
        /// Maps the value, but clamps it at the min/max borders of the output range
        /// </summary>
        Clamp,
        /// <summary>
        /// Maps the value, but repeats it into the min/max range, like a modulo function
        /// </summary>
        Wrap,
        /// <summary>
        /// Maps the value, but mirrors it into the min/max range, always against either start or end, whatever is closer
        /// </summary>
        Mirror
    };

    #endregion enums

    #region VMath class

    /// <summary>
    /// The vvvv c# math routines library
    /// </summary>
    public sealed class VMath
    {
        #region random
        /// <summary>
        /// A random object for conveninece
        /// </summary>
        public static System.Random Random = new System.Random(4444);

        /// <summary>
        /// Creates a random 2d vector.
        /// </summary>
        /// <returns>Random vector with its components in the range [0..1].</returns>
        public static Vector2D RandomVector2D()
        {
            return new Vector2D(UnityEngine.Random.Range(0f, 1f),
                                UnityEngine.Random.Range(0f, 1f));
        }

        /// <summary>
        /// Creates a random 3d vector.
        /// </summary>
        /// <returns>Random vector with its components in the range [0..1].</returns>
        public static Vector3D RandomVector3D()
        {
            return new Vector3D(UnityEngine.Random.Range(0f, 1f),
                                UnityEngine.Random.Range(0f, 1f),
                                UnityEngine.Random.Range(0f, 1f));
        }

        /// <summary>
        /// Creates a random 4d vector.
        /// </summary>
        /// <returns>Random vector with its components in the range [0..1].</returns>
        public static Vector4D RandomVector4D()
        {
            return new Vector4D(UnityEngine.Random.Range(0f, 1f),
                                UnityEngine.Random.Range(0f, 1f),
                                UnityEngine.Random.Range(0f, 1f),
                                UnityEngine.Random.Range(0f, 1f));
        }

        #endregion random

        #region numeric functions

        /// <summary>
        /// Factorial function, DON'T FEED ME WITH LARGE NUMBERS !!! (n>10 can be huge)
        /// </summary>
        /// <param name="n"></param>
        /// <returns>The product n * n-1 * n-2 * n-3 * .. * 3 * 2 * 1</returns>
        public static int Factorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            if (n < 0) { n = -n; }
            return n * Factorial(n - 1);
        }

        /// <summary>
        /// Binomial function
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns>The number of k-tuples of n items</returns>
        public static long Binomial(int n, int k)
        {
            if (n < 0) { n = -n; }
            return Factorial(n) / (Factorial(k) * Factorial(n - k));
        }


        /// <summary>
        /// Solves a quadratic equation a*x^2 + b*x + c for x
        /// </summary>
        /// <param name="a">Coefficient of x^2</param>
        /// <param name="b">Coefficient of x</param>
        /// <param name="c">Constant</param>
        /// <param name="x1">First solution</param>
        /// <param name="x2">Second solution</param>
        /// <returns>Number of solution, 0, 1, 2 or int.MaxValue</returns>
        public int SolveQuadratic(float a, float b, float c, out float x1, out float x2)
        {
            x1 = 0;
            x2 = 0;

            if (a == 0)
            {
                if ((b == 0) && (c == 0))
                {
                    return int.MaxValue;
                }
                else
                {
                    x1 = -c / b;
                    x2 = x1;
                    return 1;
                }
            }
            else
            {
                float D = b * b - 4 * a * c;

                if (D > 0)
                {

                    D = Mathf.Sqrt(D);
                    x1 = (-b + D) / (2 * a);
                    x2 = (-b - D) / (2 * a);
                    return 2;
                }
                else
                {
                    if (D == 0)
                    {
                        x1 = -b / (2 * a);
                        x2 = x1;
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        #endregion numeric functions

        #region range functions


        /// <summary>
        /// Modulo function with the property, that the remainder of a division z / d
        /// and z &lt; 0 is positive. For example: zmod(-2, 30) = 28.
        /// </summary>
        /// <param name="z"></param>
        /// <param name="d"></param>
        /// <returns>Remainder of division z / d.</returns>
        public static int Zmod(int z, int d)
        {
            if (z >= d)
                return z % d;
            else if (z < 0)
            {
                int remainder = z % d;
                return remainder == 0 ? 0 : remainder + d;
            }
            else
                return z;
        }


        /// <summary>
        /// Clamp function, clamps a floating point value into the range [min..max]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Clamp(float x, float min, float max)
        {
            float minTemp = Mathf.Min(min, max);
            float maxTemp = Mathf.Max(min, max);
            return Mathf.Min(Mathf.Max(x, minTemp), maxTemp);
        }

        /// <summary>
        /// Clamp function, clamps an integer value into the range [min..max]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Clamp(int x, int min, int max)
        {
            int minTemp = Math.Min(min, max);
            int maxTemp = Math.Max(min, max);
            return Math.Min(Math.Max(x, minTemp), maxTemp);
        }

        /// <summary>
        /// Clamp function, clamps a long value into the range [min..max]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static long Clamp(long x, long min, long max)
        {
            var minTemp = Math.Min(min, max);
            var maxTemp = Math.Max(min, max);
            return Math.Min(Math.Max(x, minTemp), maxTemp);
        }

        /// <summary>
        /// Clamp function, clamps a 2d-vector into the range [min..max]
        /// </summary>
        /// <param name="v"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector2D Clamp(Vector2D v, float min, float max)
        {
            return new Vector2D(Clamp(v.x, min, max), Clamp(v.y, min, max));
        }

        /// <summary>
        /// Clamp function, clamps a 3d-vector into the range [min..max]
        /// </summary>
        /// <param name="v"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector3D Clamp(Vector3D v, float min, float max)
        {
            return new Vector3D(Clamp(v.x, min, max), Clamp(v.y, min, max), Clamp(v.z, min, max));
        }

        /// <summary>
        /// Clamp function, clamps a 4d-vector into the range [min..max]
        /// </summary>
        /// <param name="v"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector4D Clamp(Vector4D v, float min, float max)
        {
            return new Vector4D(Clamp(v.x, min, max), Clamp(v.y, min, max), Clamp(v.z, min, max), Clamp(v.w, min, max));
        }

        /// <summary>
        /// Clamp function, clamps a 2d-vector into the range [min..max]
        /// </summary>
        /// <param name="v"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector2D Clamp(Vector2D v, Vector2D min, Vector2D max)
        {
            return new Vector2D(Clamp(v.x, min.x, max.x), Clamp(v.y, min.y, max.y));
        }

        /// <summary>
        /// Clamp function, clamps a 3d-vector into the range [min..max]
        /// </summary>
        /// <param name="v"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector3D Clamp(Vector3D v, Vector3D min, Vector3D max)
        {
            return new Vector3D(Clamp(v.x, min.x, max.x), Clamp(v.y, min.y, max.y), Clamp(v.z, min.z, max.z));
        }

        /// <summary>
        /// Clamp function, clamps a 4d-vector into the range [min..max]
        /// </summary>
        /// <param name="v"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector4D Clamp(Vector4D v, Vector4D min, Vector4D max)
        {
            return new Vector4D(Clamp(v.x, min.x, max.x), Clamp(v.y, min.y, max.y), Clamp(v.z, min.z, max.z), Clamp(v.w, min.w, max.w));
        }

        /// <summary>
        /// Abs function for values, just for completeness
        /// </summary>
        /// <param name="a"></param>
        /// <returns>New value with the absolut value of a</returns>
        public static float Abs(float a)
        {
            return Math.Abs(a);
        }

        /// <summary>
        /// Abs function for 2d-vectors
        /// </summary>
        /// <param name="a"></param>
        /// <returns>New vector with the absolut values of the components of input vector a</returns>
        public static Vector2D Abs(Vector2D a)
        {
            return new Vector2D(Math.Abs(a.x), Math.Abs(a.y));
        }

        /// <summary>
        /// Abs function for 3d-vectors
        /// </summary>
        /// <param name="a"></param>
        /// <returns>New vector with the absolut values of the components of input vector a</returns>
        public static Vector3D Abs(Vector3D a)
        {
            return new Vector3D(Math.Abs(a.x), Math.Abs(a.y), Math.Abs(a.z));
        }

        /// <summary>
        /// Abs function for 4d-vectors
        /// </summary>
        /// <param name="a"></param>
        /// <returns>New vector with the absolut values of the components of input vector a</returns>
        public static Vector4D Abs(Vector4D a)
        {
            return new Vector4D(Math.Abs(a.x), Math.Abs(a.y), Math.Abs(a.z), Math.Abs(a.w));
        }

        
        /// <summary>
        /// This Method can be seen as an inverse of Lerp (in Mode Float). Additionally it provides the infamous Mapping Modes, author: velcrome
        /// </summary>
        /// <param name="Input">Input value to convert</param>
        /// <param name="start">Minimum of input value range</param>
        /// <param name="end">Maximum of input value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="VVVV.Utils.VMath.TMapMode">TMapMode</see></param>
        /// <returns>Input value mapped from input range into destination range</returns>
        public static float Ratio(float Input, float start, float end, TMapMode mode)
        {
            if (end.CompareTo(start) == 0) return 0;

            float range = end - start;
            float ratio = (Input - start) / range;

            if (mode == TMapMode.Float) { }
            else if (mode == TMapMode.Clamp)
            {
                if (ratio < 0) ratio = 0;
                if (ratio > 1) ratio = 1;
            }
            else
            {
                if (mode == TMapMode.Wrap)
                {
                    // includes fix for inconsistent behaviour of old delphi Map 
                    // node when handling integers
                    int rangeCount = (int)Math.Floor(ratio);
                    ratio -= rangeCount;
                }
                else if (mode == TMapMode.Mirror)
                {
                    // merke: if you mirror an input twice it is displaced twice the range. same as wrapping twice really
                    int rangeCount = (int)Math.Floor(ratio);
                    rangeCount -= rangeCount & 1; // if uneven, make it even. bitmask of one is same as mod2
                    ratio -= rangeCount;

                    if (ratio > 1) ratio = 2 - ratio; // if on the max side of things now (due to rounding down rangeCount), mirror once against max
                }
            }
            return ratio;
        }

        /// <summary>
        /// The infamous Map function of vvvv for values
        /// </summary>
        /// <param name="Input">Input value to convert</param>
        /// <param name="InMin">Minimum of input value range</param>
        /// <param name="InMax">Maximum of input value range</param>
        /// <param name="OutMin">Minimum of destination value range</param>
        /// <param name="OutMax">Maximum of destination value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="VVVV.Utils.VMath.TMapMode">TMapMode</see></param>
        /// <returns>Input value mapped from input range into destination range</returns>
        public static float Map(float Input, float InMin, float InMax, float OutMin, float OutMax, TMapMode mode)
        {
            float ratio = Ratio(Input, InMin, InMax, mode);
            return Lerp(OutMin, OutMax, ratio);
        }

        /// <summary>
        /// The infamous Map function of vvvv for 2d-vectors and value range bounds
        /// </summary>
        /// <param name="Input">Input value to convert</param>
        /// <param name="InMin">Minimum of input value range</param>
        /// <param name="InMax">Maximum of input value range</param>
        /// <param name="OutMin">Minimum of destination value range</param>
        /// <param name="OutMax">Maximum of destination value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="VVVV.Utils.VMath.TMapMode">TMapMode</see></param>
        /// <returns>Input vector mapped from input range into destination range</returns>
        public static Vector2D Map(Vector2D Input, float InMin, float InMax, float OutMin, float OutMax, TMapMode mode)
        {
            return new Vector2D(Map(Input.x, InMin, InMax, OutMin, OutMax, mode),
                                Map(Input.y, InMin, InMax, OutMin, OutMax, mode));
        }

        /// <summary>
        /// The infamous Map function of vvvv for 3d-vectors and value range bounds
        /// </summary>
        /// <param name="Input">Input value to convert</param>
        /// <param name="InMin">Minimum of input value range</param>
        /// <param name="InMax">Maximum of input value range</param>
        /// <param name="OutMin">Minimum of destination value range</param>
        /// <param name="OutMax">Maximum of destination value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="VVVV.Utils.VMath.TMapMode">TMapMode</see></param>
        /// <returns>Input vector mapped from input range into destination range</returns>
        public static Vector3D Map(Vector3D Input, float InMin, float InMax, float OutMin, float OutMax, TMapMode mode)
        {
            return new Vector3D(Map(Input.x, InMin, InMax, OutMin, OutMax, mode),
                                Map(Input.y, InMin, InMax, OutMin, OutMax, mode),
                                Map(Input.z, InMin, InMax, OutMin, OutMax, mode));
        }

        /// <summary>
        /// The infamous Map function of vvvv for 4d-vectors and value range bounds
        /// </summary>
        /// <param name="Input">Input value to convert</param>
        /// <param name="InMin">Minimum of input value range</param>
        /// <param name="InMax">Maximum of input value range</param>
        /// <param name="OutMin">Minimum of destination value range</param>
        /// <param name="OutMax">Maximum of destination value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="VVVV.Utils.VMath.TMapMode">TMapMode</see></param>
        /// <returns>Input vector mapped from input range into destination range</returns>
        public static Vector4D Map(Vector4D Input, float InMin, float InMax, float OutMin, float OutMax, TMapMode mode)
        {
            return new Vector4D(Map(Input.x, InMin, InMax, OutMin, OutMax, mode),
                                Map(Input.y, InMin, InMax, OutMin, OutMax, mode),
                                Map(Input.z, InMin, InMax, OutMin, OutMax, mode),
                                Map(Input.w, InMin, InMax, OutMin, OutMax, mode));
        }

        /// <summary>
        /// The infamous Map function of vvvv for 2d-vectors and range bounds given as vectors
        /// </summary>
        /// <param name="Input">Input value to convert</param>
        /// <param name="InMin">Minimum of input value range</param>
        /// <param name="InMax">Maximum of input value range</param>
        /// <param name="OutMin">Minimum of destination value range</param>
        /// <param name="OutMax">Maximum of destination value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="VVVV.Utils.VMath.TMapMode">TMapMode</see></param>
        /// <returns>Input vector mapped from input range into destination range</returns>
        public static Vector2D Map(Vector2D Input, Vector2D InMin, Vector2D InMax, Vector2D OutMin, Vector2D OutMax, TMapMode mode)
        {
            return new Vector2D(Map(Input.x, InMin.x, InMax.x, OutMin.x, OutMax.x, mode),
                                Map(Input.y, InMin.y, InMax.y, OutMin.y, OutMax.y, mode));
        }

        /// <summary>
        /// The infamous Map function of vvvv for 3d-vectors and range bounds given as vectors
        /// </summary>
        /// <param name="Input">Input value to convert</param>
        /// <param name="InMin">Minimum of input value range</param>
        /// <param name="InMax">Maximum of input value range</param>
        /// <param name="OutMin">Minimum of destination value range</param>
        /// <param name="OutMax">Maximum of destination value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="VVVV.Utils.VMath.TMapMode">TMapMode</see></param>
        /// <returns>Input vector mapped from input range into destination range</returns>
        public static Vector3D Map(Vector3D Input, Vector3D InMin, Vector3D InMax, Vector3D OutMin, Vector3D OutMax, TMapMode mode)
        {
            return new Vector3D(Map(Input.x, InMin.x, InMax.x, OutMin.x, OutMax.x, mode),
                                Map(Input.y, InMin.y, InMax.y, OutMin.y, OutMax.y, mode),
                                Map(Input.z, InMin.z, InMax.z, OutMin.z, OutMax.z, mode));
        }

        /// <summary>
        /// The infamous Map function of vvvv for 4d-vectors and range bounds given as vectors
        /// </summary>
        /// <param name="Input">Input value to convert</param>
        /// <param name="InMin">Minimum of input value range</param>
        /// <param name="InMax">Maximum of input value range</param>
        /// <param name="OutMin">Minimum of destination value range</param>
        /// <param name="OutMax">Maximum of destination value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="VVVV.Utils.VMath.TMapMode">TMapMode</see></param>
        /// <returns>Input vector mapped from input range into destination range</returns>
        public static Vector4D Map(Vector4D Input, Vector4D InMin, Vector4D InMax, Vector4D OutMin, Vector4D OutMax, TMapMode mode)
        {
            return new Vector4D(Map(Input.x, InMin.x, InMax.x, OutMin.x, OutMax.x, mode),
                                Map(Input.y, InMin.y, InMax.y, OutMin.y, OutMax.y, mode),
                                Map(Input.z, InMin.z, InMax.z, OutMin.z, OutMax.z, mode),
                                Map(Input.w, InMin.w, InMax.w, OutMin.w, OutMax.w, mode));
        }

        #endregion range functions

        #region interpolation

        //Lerp---------------------------------------------------------------------------------------------

        /// <summary>
        /// Linear interpolation (blending) between two values
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns>Linear interpolation between a and b if x in the range ]0..1[ or a if x = 0 or b if x = 1</returns>
        public static float Lerp(float a, float b, float x)
        {
            return a + x * (b - a);
        }

        /// <summary>
        /// Linear interpolation (blending) between two 2d-vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns>Linear interpolation between a and b if x in the range ]0..1[, or a if x = 0, or b if x = 1</returns>
        public static Vector2D Lerp(Vector2D a, Vector2D b, float x)
        {
            return a + x * (b - a);
        }

        /// <summary>
        /// Linear interpolation (blending) between two 3d-vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns>Linear interpolation between a and b if x in the range ]0..1[, or a if x = 0, or b if x = 1</returns>
        public static Vector3D Lerp(Vector3D a, Vector3D b, float x)
        {
            return a + x * (b - a);
        }

        /// <summary>
        /// Linear interpolation (blending) between two 4d-vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns>Linear interpolation between a and b if x in the range ]0..1[, or a if x = 0, or b if x = 1</returns>
        public static Vector4D Lerp(Vector4D a, Vector4D b, float x)
        {
            return a + x * (b - a);
        }


        //Bilerp------------------------------------------------------------------------------------------

        /// <summary>
        /// 2d linear interpolation in x and y direction for single values
        /// </summary>
        /// <param name="Input">The position where to interpolate, 0..1</param>
        /// <param name="P1">Upper left value</param>
        /// <param name="P2">Upper right value</param>
        /// <param name="P3">Lower right value</param>
        /// <param name="P4">Lower left value</param>
        /// <returns>Interpolated value between the 4 values of the corners of a unit square</returns>
        public static float Bilerp(Vector2D Input, float P1, float P2, float P3, float P4)
        {

            //interpolate upper values in x direction
            P1 = Lerp(P1, P2, Input.x);

            //interpolate lower values in x direction
            P3 = Lerp(P4, P3, Input.x);

            //interpolate results in y direction
            return Lerp(P3, P1, Input.y);

        }

        /// <summary>
        /// 2d linear interpolation in x and y direction for 2d-vectors
        /// </summary>
        /// <param name="Input">The position where to interpolate, 0..1</param>
        /// <param name="P1">Upper left vector</param>
        /// <param name="P2">Upper right vector</param>
        /// <param name="P3">Lower right vector</param>
        /// <param name="P4">Lower left vector</param>
        /// <returns>Interpolated vector between the 4 vectors of the corners of a unit square</returns>
        public static Vector2D Bilerp(Vector2D Input, Vector2D P1, Vector2D P2, Vector2D P3, Vector2D P4)
        {

            //interpolate upper points in x direction
            P1 = Lerp(P1, P2, Input.x);

            //interpolate lower points in x direction
            P3 = Lerp(P4, P3, Input.x);

            //interpolate results in y direction
            return Lerp(P3, P1, Input.y);

        }

        /// <summary>
        /// 2d linear interpolation in x and y direction for 3d-vectors
        /// </summary>
        /// <param name="Input">The position where to interpolate, 0..1</param>
        /// <param name="P1">Upper left vector</param>
        /// <param name="P2">Upper right vector</param>
        /// <param name="P3">Lower right vector</param>
        /// <param name="P4">Lower left vector</param>
        /// <returns>Interpolated vector between the 4 vectors of the corners of a unit square</returns>
        public static Vector3D Bilerp(Vector2D Input, Vector3D P1, Vector3D P2, Vector3D P3, Vector3D P4)
        {

            //interpolate upper points in x direction
            P1 = Lerp(P1, P2, Input.x);

            //interpolate lower points in x direction
            P3 = Lerp(P4, P3, Input.x);

            //interpolate results in y direction
            return Lerp(P3, P1, Input.y);

        }

        /// <summary>
        /// 2d linear interpolation in x and y direction for 4d-vectors
        /// </summary>
        /// <param name="Input">The position where to interpolate, 0..1</param>
        /// <param name="P1">Upper left vector</param>
        /// <param name="P2">Upper right vector</param>
        /// <param name="P3">Lower right vector</param>
        /// <param name="P4">Lower left vector</param>
        /// <returns>Interpolated vector between the 4 vectors of the corners of a unit square</returns>
        public static Vector4D Bilerp(Vector2D Input, Vector4D P1, Vector4D P2, Vector4D P3, Vector4D P4)
        {

            //interpolate upper points in x direction
            P1 = Lerp(P1, P2, Input.x);

            //interpolate lower points in x direction
            P3 = Lerp(P4, P3, Input.x);

            //interpolate results in y direction
            return Lerp(P3, P1, Input.y);

        }


        //Trilerp-------------------------------------------------------------------------------

        /// <summary>
        /// 3d linear interpolation in x, y and z direction for single values
        /// </summary>
        /// <param name="Input">The Interpolation factor, 3d-position inside the unit cube</param>
        /// <param name="V010">Front upper left</param>
        /// <param name="V110">Front upper right</param>
        /// <param name="V100">Front lower right</param>
        /// <param name="V000">Front lower left</param>
        /// <param name="V011">Back upper left</param>
        /// <param name="V111">Back upper right</param>
        /// <param name="V101">Back lower right</param>
        /// <param name="V001">Back lower left</param>
        /// <returns>Interpolated value between the 8 values of the corners of a unit cube</returns>
        public static float Trilerp(Vector3D Input,
                                     float V010, float V110, float V100, float V000,
                                     float V011, float V111, float V101, float V001)
        {
            Vector2 xy = new Vector2(Input.x, Input.y);

            //interpolate the front side
            V000 = Bilerp(xy, V010, V110, V100, V000);

            //interpolate the back side
            V111 = Bilerp(xy, V011, V111, V101, V001);

            //interpolate in z direction
            return Lerp(V000, V111, Input.z);
        }

        /// <summary>
        /// 3d linear interpolation in x, y and z direction for 2d-vectors
        /// </summary>
        /// <param name="Input">The Interpolation factor, 3d-position inside the unit cube</param>
        /// <param name="V010">Front upper left</param>
        /// <param name="V110">Front upper right</param>
        /// <param name="V100">Front lower right</param>
        /// <param name="V000">Front lower left</param>
        /// <param name="V011">Back upper left</param>
        /// <param name="V111">Back upper right</param>
        /// <param name="V101">Back lower right</param>
        /// <param name="V001">Back lower left</param>
        /// <returns>Interpolated vector between the 8 vectors of the corners of a unit cube</returns>
        public static Vector2D Trilerp(Vector3D Input,
                                     Vector2D V010, Vector2D V110, Vector2D V100, Vector2D V000,
                                     Vector2D V011, Vector2D V111, Vector2D V101, Vector2D V001)
        {
            Vector2 xy = new Vector2(Input.x, Input.y);

            //interpolate the front side
            V000 = Bilerp(xy, V010, V110, V100, V000);

            //interpolate the back side
            V111 = Bilerp(xy, V011, V111, V101, V001);

            //interpolate in z direction
            return Lerp(V000, V111, Input.z);
        }

        /// <summary>
        /// 3d linear interpolation in x, y and z direction for 3d-vectors
        /// </summary>
        /// <param name="Input">The Interpolation factor, 3d-position inside the unit cube</param>
        /// <param name="V010">Front upper left</param>
        /// <param name="V110">Front upper right</param>
        /// <param name="V100">Front lower right</param>
        /// <param name="V000">Front lower left</param>
        /// <param name="V011">Back upper left</param>
        /// <param name="V111">Back upper right</param>
        /// <param name="V101">Back lower right</param>
        /// <param name="V001">Back lower left</param>
        /// <returns>Interpolated vector between the 8 vectors of the corners of a unit cube</returns>
        public static Vector3D Trilerp(Vector3D Input,
                                     Vector3D V010, Vector3D V110, Vector3D V100, Vector3D V000,
                                     Vector3D V011, Vector3D V111, Vector3D V101, Vector3D V001)
        {
            Vector2 xy = new Vector2(Input.x, Input.y);

            //interpolate the front side
            V000 = Bilerp(xy, V010, V110, V100, V000);

            //interpolate the back side
            V111 = Bilerp(xy, V011, V111, V101, V001);

            //interpolate in z direction
            return Lerp(V000, V111, Input.z);
        }

        /// <summary>
        /// 3d linear interpolation in x, y and z direction for 4d-vectors
        /// </summary>
        /// <param name="Input">The Interpolation factor, 3d-position inside the unit cube</param>
        /// <param name="V010">Front upper left</param>
        /// <param name="V110">Front upper right</param>
        /// <param name="V100">Front lower right</param>
        /// <param name="V000">Front lower left</param>
        /// <param name="V011">Back upper left</param>
        /// <param name="V111">Back upper right</param>
        /// <param name="V101">Back lower right</param>
        /// <param name="V001">Back lower left</param>
        /// <returns>Interpolated vector between the 8 vectors of the corners of a unit cube</returns>
        public static Vector4D Trilerp(Vector3D Input,
                                     Vector4D V010, Vector4D V110, Vector4D V100, Vector4D V000,
                                     Vector4D V011, Vector4D V111, Vector4D V101, Vector4D V001)
        {
            Vector2 xy = new Vector2(Input.x, Input.y);

            //interpolate the front side
            V000 = Bilerp(xy, V010, V110, V100, V000);

            //interpolate the back side
            V111 = Bilerp(xy, V011, V111, V101, V001);

            //interpolate in z direction
            return Lerp(V000, V111, Input.z);
        }

        //cubic---------------------------------------------------------------------------------------------

        /// <summary>
        /// Cubic interpolation curve used in the vvvv timeline
        /// </summary>
        /// <param name="CurrenTime"></param>
        /// <param name="Handle0"></param>
        /// <param name="Handle1"></param>
        /// <param name="Handle2"></param>
        /// <param name="Handle3"></param>
        /// <returns></returns>
        public static float SolveCubic(float CurrenTime, float Handle0, float Handle1, float Handle2, float Handle3)
        {
            return (Handle0 * (Mathf.Pow((1 - CurrenTime), 3)) + (3 * Handle1) * (CurrenTime * Mathf.Pow((1 - CurrenTime), 2)) + (3 * Handle2) * (Mathf.Pow(CurrenTime, 2) * (1 - CurrenTime)) + Handle3 * Mathf.Pow(CurrenTime, 3));
        }


        //spherical-----------------------------------------------------------------------------------------

        /// <summary>
        /// Spherical interpolation between two quaternions (4d-vectors)
        /// The effect is a rotation with uniform angular velocity around a fixed rotation axis from one state of rotation to another
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns>Spherical interpolation between a and b if x in the range ]0..1[, or a if x = 0, or b if x = 1</returns>
        public static Vector4D Slerp(Vector4D a, Vector4D b, float x)
        {
            float w1, w2;

            float cosTheta = Vector4.Dot(a, b); // | is dot product
            float theta = Mathf.Acos(cosTheta);
            float sinTheta = Mathf.Sin(theta);

            if (sinTheta > 0.0001)
            {
                sinTheta = 1 / sinTheta;
                w1 = Mathf.Sin((1 - x) * theta) * sinTheta;
                w2 = Mathf.Sin(x * theta) * sinTheta;
            }
            else
            {
                w1 = 1 - x;
                w2 = x;
            }

            return a * w1 + b * w2;
        }

        /// <summary>
        /// Spherical interpolation between two points (3d-vectors)
        /// The effect is a rotation with uniform angular velocity around a fixed rotation axis from one state of rotation to another
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns>Spherical interpolation between a and b if x in the range ]0..1[, or a if x = 0, or b if x = 1</returns>
        public static Vector3D Slerp(Vector3D a, Vector3D b, float x)
        {
            float w1, w2;
            float theta, sinTheta;

            float cosTheta = Vector3.Dot(a, b); // a | b;  // | is dot product
            float len = Mathf.Sqrt((Vector3.Dot(a, a)) * (Vector3.Dot(b, b)));   //len = length(A) * length(B)

            if (len > 0.0001)
            {
                theta = Mathf.Acos(cosTheta / len);
                sinTheta = Mathf.Sin(theta);

                if (sinTheta > 0.0001)
                {
                    sinTheta = 1 / sinTheta;
                    w1 = Mathf.Sin((1 - x) * theta) * sinTheta;
                    w2 = Mathf.Sin(x * theta) * sinTheta;
                }
                else
                {
                    w1 = 1 - x;
                    w2 = x;
                }
            }
            else
            {
                w1 = 1 - x;
                w2 = x;
            }

            return a * w1 + b * w2;
        }

        #endregion interpolation

        #region 3D functions

        /// <summary>
        /// Convert polar coordinates (pitch, yaw, lenght) in radian to cartesian coordinates (x, y, z).
        /// To convert angles from cycles to radian, multiply them with VMath.CycToDec.
        /// </summary>
        /// <param name="pitch"></param>
        /// <param name="yaw"></param>
        /// <param name="length"></param>
        /// <returns>3d-point in cartesian coordinates</returns>
        public static Vector3D Cartesian(float pitch, float yaw, float length)
        {
            float sinp = length * Mathf.Sin(pitch);

            return new Vector3D(sinp * Mathf.Cos(yaw), sinp * Mathf.Sin(yaw), length * Mathf.Cos(pitch));
        }

        /// <summary>
        /// Convert polar coordinates (pitch, yaw, lenght) in radian to cartesian coordinates (x, y, z).
        /// To convert angles from cycles to radian, multiply them with VMath.CycToDec.
        /// </summary>
        /// <param name="polar">3d-vector containing the polar coordinates as (pitch, yaw, length)</param>
        /// <returns></returns>
        public static Vector3D Cartesian(Vector3D polar)
        {
            float sinp = polar.z * Mathf.Sin(polar.x);

            return new Vector3D(sinp * Mathf.Cos(polar.y), sinp * Mathf.Sin(polar.y), polar.z * Mathf.Cos(polar.x));
        }

        /// <summary>
        /// Convert polar coordinates (pitch, yaw, lenght) in radian to cartesian coordinates (x, y, z) exacly like the vvvv node Cartesian.
        /// To convert angles from cycles to radian, multiply them with VMath.CycToDec.
        /// </summary>
        /// <param name="pitch"></param>
        /// <param name="yaw"></param>
        /// <param name="length"></param>
        /// <returns>3d-point in cartesian coordinates like the vvvv node does it</returns>
        public static Vector3D CartesianVVVV(float pitch, float yaw, float length)
        {
            float cosp = -length * Mathf.Cos(pitch);

            return new Vector3D(cosp * Mathf.Sin(yaw), length * Mathf.Sin(pitch), cosp * Mathf.Cos(yaw));
        }

        /// <summary>
        /// Convert polar coordinates (pitch, yaw, lenght) in radian to cartesian coordinates (x, y, z) exacly like the vvvv node Cartesian.
        /// To convert angles from cycles to radian, multiply them with VMath.CycToDec.
        /// </summary>
        /// <param name="polar">3d-vector containing the polar coordinates as (pitch, yaw, length)</param>
        /// <returns></returns>
        public static Vector3D CartesianVVVV(Vector3D polar)
        {
            float cosp = -polar.z * Mathf.Cos(polar.x);

            return new Vector3D(cosp * Mathf.Sin(polar.y), polar.z * Mathf.Sin(polar.x), cosp * Mathf.Cos(polar.y));
        }

        /// <summary>
        /// Convert cartesian coordinates (x, y, z) to polar coordinates (pitch, yaw, lenght) in radian.
        /// To convert the angles to cycles, multiply them with VMath.DegToCyc.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>3d-point in polar coordinates</returns>
        public static Vector3D Polar(float x, float y, float z)
        {
            float length = x * x + y * y + z * z;


            if (length > 0)
            {
                length = Mathf.Sqrt(length);
                return new Vector3D(Mathf.Acos(z / length), Mathf.Atan2(y, x), length);
            }
            else
            {
                return Vector3.zero;
            }

        }

        /// <summary>
        /// Convert cartesian coordinates (x, y, z) to polar coordinates (pitch, yaw, lenght) in radian.
        /// To convert the angles to cycles, multiply them with VMath.DegToCyc.
        /// </summary>
        /// <param name="a"></param>
        /// <returns>Point in polar coordinates</returns>
        public static Vector3D Polar(Vector3D a)
        {
            float length = a.x * a.x + a.y * a.y + a.z * a.z;


            if (length > 0)
            {
                length = Mathf.Sqrt(length);
                return new Vector3D(Mathf.Acos(a.z / length), Mathf.Atan2(a.y, a.x), length);
            }
            else
            {
                return Vector3.zero;
            }

        }

        /// <summary>
        /// Convert cartesian coordinates (x, y, z) to VVVV style polar coordinates (pitch, yaw, lenght) in radian.
        /// To convert the angles to cycles, multiply them with VMath.DegToCyc.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>3d-point in polar coordinates</returns>
        public static Vector3D PolarVVVV(float x, float y, float z)
        {
            float length = x * x + y * y + z * z;


            if (length > 0)
            {
                length = Mathf.Sqrt(length);
                float pitch = Mathf.Asin(y / length);
                float yaw = 0.0f;
                if (z != 0)
                    yaw = Mathf.Atan2(-x, -z);
                else if (x > 0)
                    yaw = -Mathf.PI / 2;
                else
                    yaw = Mathf.PI / 2;

                return new Vector3D(pitch, yaw, length);
            }
            else
            {
                return Vector3.zero;
            }

        }

        /// <summary>
        /// Convert cartesian coordinates (x, y, z) to polar VVVV style coordinates (pitch, yaw, lenght) in radian.
        /// To convert the angles to cycles, multiply them with VMath.DegToCyc.
        /// </summary>
        /// <param name="a"></param>
        /// <returns>Point in polar coordinates</returns>
        public static Vector3D PolarVVVV(Vector3D a)
        {
            float length = a.x * a.x + a.y * a.y + a.z * a.z;


            if (length > 0)
            {
                length = Mathf.Sqrt(length);
                float pitch = Mathf.Asin(a.y / length);
                float yaw = 0.0f;
                if (a.z != 0)
                    yaw = Mathf.Atan2(-a.x, -a.z);
                else if (a.x > 0)
                    yaw = -Mathf.PI / 2;
                else
                    yaw = Mathf.PI / 2;

                return new Vector3D(pitch, yaw, length);
            }
            else
            {
                return Vector3D.zero;
            }

        }

        /// <summary>
        /// Converts a quaternion into euler angles, assuming that the euler angle multiplication to create the quaternion was yaw*pitch*roll.
        /// All angles in radian.
        /// </summary>
        /// <param name="q">A quaternion, can be non normalized</param>
        /// <param name="pitch"></param>
        /// <param name="yaw"></param>
        /// <param name="roll"></param>
        public static void QuaternionToEulerYawPitchRoll(Vector4D q, out float pitch, out float yaw, out float roll)
        {
            float sqw = q.w * q.w;
            float sqx = q.x * q.x;
            float sqy = q.y * q.y;
            float sqz = q.z * q.z;
            float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
            float test = q.x * q.w - q.y * q.z;

            if (test > 0.4999 * unit)
            { // singularity at north pole
                pitch = Mathf.PI / 2;
                yaw = 2 * Mathf.Atan2(q.y, q.w);
                roll = 0;
                return;
            }

            if (test < -0.4999 * unit)
            { // singularity at south pole
                pitch = -Mathf.PI / 2;
                yaw = -2 * Mathf.Atan2(q.y, q.w);
                roll = 0;
                return;
            }

            pitch = Mathf.Asin(2 * (q.w * q.x - q.y * q.z) / unit);
            yaw = Mathf.Atan2(2 * (q.w * q.y + q.x * q.z), 1 - 2 * (sqy + sqx));
            roll = Mathf.Atan2(2 * (q.w * q.z + q.y * q.x), 1 - 2 * (sqx + sqz));
        }

        /// <summary>
        /// Converts a quaternion into euler angles, assuming that the euler angle multiplication to create the quaternion was yaw*pitch*roll.
        /// All angles in radian.
        /// </summary>
        /// <param name="q">A quaternion, can be non normalized</param>
        /// <returns>3d-vector with x=pitch, y=yaw, z=roll</returns>
        public static Vector3D QuaternionToEulerYawPitchRoll(Vector4D q)
        {
            Vector3D ret;

            QuaternionToEulerYawPitchRoll(q, out ret.x, out ret.y, out ret.z);

            return ret;
        }

        /// <summary>
        /// Intersaction of 3 Spheres
        /// </summary>
        /// <param name="P1">Center sphere 1</param>
        /// <param name="P2">Center sphere 2</param>
        /// <param name="P3">Center sphere 3</param>
        /// <param name="r1">Radius sphere 1</param>
        /// <param name="r2">Radius sphere 2</param>
        /// <param name="r3">Radius sphere 3</param>
        /// <param name="S1">Intersection Point 1</param>
        /// <param name="S2">Intersection Point 2</param>
        /// <returns>Number of intersections</returns>
        public static int Trilateration(Vector3D P1, Vector3D P2, Vector3D P3, float r1, float r2, float r3, out Vector3D S1, out Vector3D S2)
        {

            //P1 to P2 vector
            var P1toP2 = P2 - P1;

            //distance P1 to P2
            var dsqr = Vector3.Dot(P1toP2, P1toP2); // d^2 needed later
            var d = Mathf.Sqrt(dsqr);

            //assume, that sphere 1 and 2 intersect
            if ((d - r1 <= r2) && (r2 <= d + r1))
            {

                //P1 to P3 vector
                var P1toP3 = P3 - P1;

                //normal base x
                var ex = P1toP2 / d;

                //distance P1 to P3 in direction to P2
                var i = Vector3.Dot(ex, P1toP3);

                //normal base y
                var ey = (P3 - P1 - i * ex).normalized; //~(P3 - P1 - i * ex);

                //distance P1P2 orthoganl to P3
                var j = Vector3.Dot(ey, P1toP3); //ey | P1toP3;

                //normal base z
                var ez = Vector3.Cross(ex, ey); //ex.CrossRH(ey);

                //calc x
                var r1sqr = r1 * r1;
                var x = (r1sqr - r2 * r2 + dsqr) / (2 * d);

                //calc y
                var xmini = x - i;
                var xsqr = x * x;
                var y = (r1sqr - r3 * r3 - xsqr + xmini * xmini + j * j) / (2 * j);

                //calc z
                var z = Mathf.Sqrt(r1sqr - xsqr - y * y);

                var zez = z * ez;
                S1 = P1 + x * ex + y * ey + zez;
                S2 = S1 - 2 * zez;

                return 2;
            }
            else
            {
                S1 = new Vector3D();
                S2 = new Vector3D();
                return 0;
            }

        }


        #endregion 3D functions

        #region transforms

        /// <summary>
        /// Creates a translation matrix from 3 given values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>Translation matrix</returns>
        public static Matrix4x4 Translate(float x, float y, float z)
        {

            //return new Matrix4x4(1, 0, 0, 0,
            //                     0, 1, 0, 0,
            //                     0, 0, 1, 0,
            //                     x, y, z, 1);
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4D(1, 0, 0, 0));
            m.SetRow(1, new Vector4D(0, 1, 0, 0));
            m.SetRow(2, new Vector4D(0, 0, 1, 0));
            m.SetRow(3, new Vector4D(x, y, z, 1));
            return m;
        }

        /// <summary>
        /// Creates a translation matrix from a given 3d-vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns>Translation matrix</returns>
        public static Matrix4x4 Translate(Vector3D v)
        {
            //return new Matrix4x4(1, 0, 0, 0,
            //                     0, 1, 0, 0,
            //                     0, 0, 1, 0,
            //                     v.x, v.y, v.z, 1);
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4D(1, 0, 0, 0));
            m.SetRow(1, new Vector4D(0, 1, 0, 0));
            m.SetRow(2, new Vector4D(0, 0, 1, 0));
            m.SetRow(3, new Vector4D(v.x, v.y, v.z, 1));
            return m;
        }

        /// <summary>
        /// Creates a scaling matrix from 3 given values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>Scaling matrix</returns>
        public static Matrix4x4 Scale(float x, float y, float z)
        {
            //return new Matrix4x4(x, 0, 0, 0,
            //                     0, y, 0, 0,
            //                     0, 0, z, 0,
            //                     0, 0, 0, 1);
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4D(x, 0, 0, 0));
            m.SetRow(1, new Vector4D(0, y, 0, 0));
            m.SetRow(2, new Vector4D(0, 0, z, 0));
            m.SetRow(3, new Vector4D(0, 0, 0, 1));
            return m;
        }

        /// <summary>
        /// Creates a scaling matrix from a given 3d-vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns>Scaling matrix</returns>
        public static Matrix4x4 Scale(Vector3D v)
        {
            //return new Matrix4x4(v.x, 0, 0, 0,
            //                       0, v.y, 0, 0,
            //                       0, 0, v.z, 0,
            //                       0, 0, 0, 1);
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4D(v.x, 0, 0, 0));
            m.SetRow(1, new Vector4D(0, v.y, 0, 0));
            m.SetRow(2, new Vector4D(0, 0, v.z, 0));
            m.SetRow(3, new Vector4D(0, 0, 0, 1));
            return m;
        }

        /// <summary>
        /// Creates a rotation matrix from a given angle around the x-axis
        /// </summary>
        /// <param name="rotX"></param>
        /// <returns>Rotation matrix</returns>
        public static Matrix4x4 RotateX(float rotX)
        {
            float s = Mathf.Sin(rotX);
            float c = Mathf.Cos(rotX);

            //return new Matrix4x4(1, 0, 0, 0,
            //                     0, c, s, 0,
            //                     0, -s, c, 0,
            //                     0, 0, 0, 1);
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4D(1, 0, 0, 0));
            m.SetRow(1, new Vector4D(0, c, s, 0));
            m.SetRow(2, new Vector4D(0, -s, c, 0));
            m.SetRow(3, new Vector4D(0, 0, 0, 1));
            return m;
        }

        /// <summary>
        /// Creates a rotation matrix from a given angle around the y-axis
        /// </summary>
        /// <param name="rotY"></param>
        /// <returns>Rotation matrix</returns>
        public static Matrix4x4 RotateY(float rotY)
        {
            float s = Mathf.Sin(rotY);
            float c = Mathf.Cos(rotY);

            //return new Matrix4x4(c, 0, -s, 0,
            //                     0, 1, 0, 0,
            //                     s, 0, c, 0,
            //                     0, 0, 0, 1);
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4D(c, 0, -s, 0));
            m.SetRow(1, new Vector4D(0, 1, 0, 0));
            m.SetRow(2, new Vector4D(s, 0, c, 0));
            m.SetRow(3, new Vector4D(0, 0, 0, 1));
            return m;
        }

        /// <summary>
        /// Creates a rotation matrix from a given angle around the z-axis
        /// </summary>
        /// <param name="rotZ"></param>
        /// <returns>Rotation matrix</returns>
        public static Matrix4x4 RotateZ(float rotZ)
        {
            float s = Mathf.Sin(rotZ);
            float c = Mathf.Cos(rotZ);

            //return new Matrix4x4(c, s, 0, 0,
            //                     -s, c, 0, 0,
            //                      0, 0, 1, 0,
            //                      0, 0, 0, 1);
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4D(c, s, 0, 0));
            m.SetRow(1, new Vector4D(-s, c, 0, 0));
            m.SetRow(2, new Vector4D(0, 0, 1, 0));
            m.SetRow(3, new Vector4D(0, 0, 0, 1));
            return m;
        }

        /// <summary>
        /// Creates a rotation matrix from 3 angles
        /// </summary>
        /// <param name="rotX"></param>
        /// <param name="rotY"></param>
        /// <param name="rotZ"></param>
        /// <returns>Rotation matrix</returns>
        public static Matrix4x4 Rotate(float rotX, float rotY, float rotZ)
        {
            float sx = Mathf.Sin(rotX);
            float cx = Mathf.Cos(rotX);
            float sy = Mathf.Sin(rotY);
            float cy = Mathf.Cos(rotY);
            float sz = Mathf.Sin(rotZ);
            float cz = Mathf.Cos(rotZ);

            //return new Matrix4x4(cz * cy + sz * sx * sy, sz * cx, cz * -sy + sz * sx * cy, 0,
            //                     -sz * cy + cz * sx * sy, cz * cx, sz * sy + cz * sx * cy, 0,
            //                                     cx * sy, -sx, cx * cy, 0,
            //                                           0, 0, 0, 1);
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4D(cz * cy + sz * sx * sy, sz * cx, cz * -sy + sz * sx * cy, 0));
            m.SetRow(1, new Vector4D(-sz * cy + cz * sx * sy, cz * cx, sz * sy + cz * sx * cy, 0));
            m.SetRow(2, new Vector4D(cx * sy, -sx, cx * cy, 0));
            m.SetRow(3, new Vector4D(0, 0, 0, 1));
            return m;
        }

        /// <summary>
        ///  Creates a rotation matrix from 3 angles given as 3d-vector
        /// </summary>
        /// <param name="rot"></param>
        /// <returns>Rotation matrix</returns>
        public static Matrix4x4 Rotate(Vector3D rot)
        {
            float sx = Mathf.Sin(rot.x);
            float cx = Mathf.Cos(rot.x);
            float sy = Mathf.Sin(rot.y);
            float cy = Mathf.Cos(rot.y);
            float sz = Mathf.Sin(rot.z);
            float cz = Mathf.Cos(rot.z);

            //return new Matrix4x4(cz * cy + sz * sx * sy, sz * cx, cz * -sy + sz * sx * cy, 0,
            //                     -sz * cy + cz * sx * sy, cz * cx, sz * sy + cz * sx * cy, 0,
            //                                     cx * sy, -sx, cx * cy, 0,
            //                                           0, 0, 0, 1);
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4D(cz * cy + sz * sx * sy, sz * cx, cz * -sy + sz * sx * cy, 0));
            m.SetRow(1, new Vector4D(-sz * cy + cz * sx * sy, cz * cx, sz * sy + cz * sx * cy, 0));
            m.SetRow(2, new Vector4D(cx * sy, -sx, cx * cy, 0));
            m.SetRow(3, new Vector4D(0, 0, 0, 1));
            return m;
        }

        /// <summary>
        /// Creates a transform matrix from translation, scaling and rotation parameters
        /// </summary>
        /// <param name="transX"></param>
        /// <param name="transY"></param>
        /// <param name="transZ"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        /// <param name="scaleZ"></param>
        /// <param name="rotX"></param>
        /// <param name="rotY"></param>
        /// <param name="rotZ"></param>
        /// <returns>Transform matrix</returns>
        public static Matrix4x4 Transform(float transX, float transY, float transZ,
                                          float scaleX, float scaleY, float scaleZ,
                                          float rotX, float rotY, float rotZ)
        {
            return Rotate(rotX, rotY, rotZ) * Scale(scaleX, scaleY, scaleZ) * Translate(transX, transY, transZ);
        }

        /// <summary>
        /// Creates a transform matrix from translation, scaling and rotation parameters given as 3d-vectors
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="scale"></param>
        /// <param name="rot"></param>
        /// <returns>Transform matrix</returns>
        public static Matrix4x4 Transform(Vector3D trans, Vector3D scale, Vector3D rot)
        {
            return Rotate(rot.x, rot.y, rot.z) * Scale(scale.x, scale.y, scale.z) * Translate(trans.x, trans.y, trans.z);
        }

        /// <summary>
        /// Creates a transform matrix from translation, scaling and rotation parameters
        /// Like the vvvv node Transform (3d)
        /// </summary>
        /// <param name="transX"></param>
        /// <param name="transY"></param>
        /// <param name="transZ"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        /// <param name="scaleZ"></param>
        /// <param name="rotX"></param>
        /// <param name="rotY"></param>
        /// <param name="rotZ"></param>
        /// <returns>Transform matrix</returns>
        public static Matrix4x4 TransformVVVV(float transX, float transY, float transZ,
                                              float scaleX, float scaleY, float scaleZ,
                                              float rotX, float rotY, float rotZ)
        {
            return Scale(scaleX, scaleY, scaleZ) * Rotate(rotX, rotY, rotZ) * Translate(transX, transY, transZ);
        }

        /// <summary>
        /// Creates a transform matrix from translation, scaling and rotation parameters given as 3d-vectors
        /// Like the vvvv node Transform (3d Vector)
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="scale"></param>
        /// <param name="rot"></param>
        /// <returns>Transform matrix</returns>
        public static Matrix4x4 TransformVVVV(Vector3D trans, Vector3D scale, Vector3D rot)
        {
            return Scale(scale.x, scale.y, scale.z) * Rotate(rot.x, rot.y, rot.z) * Translate(trans.x, trans.y, trans.z);
        }

        /// <summary>
        /// Builds a left-handed perspective projection matrix based on a field of view.
        /// </summary>
        /// <param name="FOV">Camera angle in cycles, [0..0.5]</param>
        /// <param name="Near">Near Plane z</param>
        /// <param name="Far">Far Plane z</param>
        /// <param name="Aspect">Aspect Ratio</param>
        /// <returns>Projection matrix</returns>
        public static Matrix4x4 PerspectiveLH(float FOV, float Near, float Far, float Aspect)
        {
            float scaleY = 1.0f / Mathf.Tan(FOV * Mathf.PI);
            float scaleX = scaleY / Aspect;
            float fn = Far / (Far - Near);

            //return new Matrix4x4(scaleX, 0, 0, 0,
            //                          0, scaleY, 0, 0,
            //                          0, 0, fn, 1,
            //                          0, 0, -Near * fn, 0);
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4D(scaleX, 0, 0, 0));
            m.SetRow(1, new Vector4D(0, scaleY, 0, 0));
            m.SetRow(2, new Vector4D(0, 0, fn, 1));
            m.SetRow(3, new Vector4D(0, 0, -Near * fn, 0));
            return m;

        }

        /// <summary>
        /// Builds a right-handed perspective projection matrix based on a field of view.
        /// </summary>
        /// <param name="FOV">Camera angle in cycles, [0..0.5]</param>
        /// <param name="Near">Near Plane z</param>
        /// <param name="Far">Far Plane z</param>
        /// <param name="Aspect">Aspect Ratio</param>
        /// <returns>Projection matrix</returns>
        public static Matrix4x4 PerspectiveRH(float FOV, float Near, float Far, float Aspect)
        {
            float scaleY = 1.0f / Mathf.Tan(FOV * Mathf.PI);
            float scaleX = scaleY / Aspect;
            float fn = Far / (Far - Near);

            //return new Matrix4x4(scaleX, 0, 0, 0,
            //                          0, scaleY, 0, 0,
            //                          0, 0, fn, -1,
            //                          0, 0, Near * fn, 0);
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4D(scaleX, 0, 0, 0));
            m.SetRow(1, new Vector4D(0, scaleY, 0, 0));
            m.SetRow(2, new Vector4D(0, 0, fn, -1));
            m.SetRow(3, new Vector4D(0, 0, Near * fn, 0));
            return m;

        }

        /// <summary>
        /// Transpose a 4x4 matrix
        /// </summary>
        /// <param name="A"></param>
        /// <returns>Transpose of input matrix A</returns>
        public static Matrix4x4 Transpose(Matrix4x4 A)
        {
            //return new Matrix4x4(A.m11, A.m21, A.m31, A.m41,
            //                     A.m12, A.m22, A.m32, A.m42,
            //                     A.m13, A.m23, A.m33, A.m43,
            //                     A.m14, A.m24, A.m34, A.m44);
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4D(A.m00, A.m10, A.m20, A.m30));
            m.SetRow(1, new Vector4D(A.m01, A.m11, A.m21, A.m31));
            m.SetRow(2, new Vector4D(A.m02, A.m12, A.m22, A.m32));
            m.SetRow(3, new Vector4D(A.m03, A.m13, A.m23, A.m33));
            
            return m;
        }

        /// <summary>
        /// Optimized 4x4 matrix inversion using cramer's rule, found in the game engine http://www.ogre3d.org
        /// Note that the unary ! operator of Matrix4x4 does the same
        /// 
        /// Code takes about 1,8ns to execute on intel core2 duo 2Ghz, the intel reference
        /// implementation (not assembly optimized) was about 2,2ns.
        /// http://www.intel.com/design/pentiumiii/sml/24504301.pdf
        /// </summary>
        /// <param name="A"></param>
        /// <returns>Inverse matrix of input matrix A</returns>
        public static Matrix4x4 Inverse(Matrix4x4 A)
        {
            float a11 = A.m00, a12 = A.m01, a13 = A.m02, a14 = A.m03;
            float a21 = A.m10, a22 = A.m11, a23 = A.m12, a24 = A.m13;
            float a31 = A.m20, a32 = A.m21, a33 = A.m22, a34 = A.m23;
            float a41 = A.m30, a42 = A.m31, a43 = A.m32, a44 = A.m33;

            float term1 = a31 * a42 - a32 * a41;
            float term2 = a31 * a43 - a33 * a41;
            float term3 = a31 * a44 - a34 * a41;
            float term4 = a32 * a43 - a33 * a42;
            float term5 = a32 * a44 - a34 * a42;
            float term6 = a33 * a44 - a34 * a43;

            float subterm1 = +(term6 * a22 - term5 * a23 + term4 * a24);
            float subterm2 = -(term6 * a21 - term3 * a23 + term2 * a24);
            float subterm3 = +(term5 * a21 - term3 * a22 + term1 * a24);
            float subterm4 = -(term4 * a21 - term2 * a22 + term1 * a23);

            float invDet = 1 / (subterm1 * a11 + subterm2 * a12 + subterm3 * a13 + subterm4 * a14);

            float ret11 = subterm1 * invDet;
            float ret21 = subterm2 * invDet;
            float ret31 = subterm3 * invDet;
            float ret41 = subterm4 * invDet;

            float ret12 = -(term6 * a12 - term5 * a13 + term4 * a14) * invDet;
            float ret22 = +(term6 * a11 - term3 * a13 + term2 * a14) * invDet;
            float ret32 = -(term5 * a11 - term3 * a12 + term1 * a14) * invDet;
            float ret42 = +(term4 * a11 - term2 * a12 + term1 * a13) * invDet;

            term1 = a21 * a42 - a22 * a41;
            term2 = a21 * a43 - a23 * a41;
            term3 = a21 * a44 - a24 * a41;
            term4 = a22 * a43 - a23 * a42;
            term5 = a22 * a44 - a24 * a42;
            term6 = a23 * a44 - a24 * a43;

            float ret13 = +(term6 * a12 - term5 * a13 + term4 * a14) * invDet;
            float ret23 = -(term6 * a11 - term3 * a13 + term2 * a14) * invDet;
            float ret33 = +(term5 * a11 - term3 * a12 + term1 * a14) * invDet;
            float ret43 = -(term4 * a11 - term2 * a12 + term1 * a13) * invDet;

            term1 = a32 * a21 - a31 * a22;
            term2 = a33 * a21 - a31 * a23;
            term3 = a34 * a21 - a31 * a24;
            term4 = a33 * a22 - a32 * a23;
            term5 = a34 * a22 - a32 * a24;
            term6 = a34 * a23 - a33 * a24;

            float ret14 = -(term6 * a12 - term5 * a13 + term4 * a14) * invDet;
            float ret24 = +(term6 * a11 - term3 * a13 + term2 * a14) * invDet;
            float ret34 = -(term5 * a11 - term3 * a12 + term1 * a14) * invDet;
            float ret44 = +(term4 * a11 - term2 * a12 + term1 * a13) * invDet;

            //return new Matrix4x4(ret11, ret12, ret13, ret14,
            //                     ret21, ret22, ret23, ret24,
            //                     ret31, ret32, ret33, ret34,
            //                     ret41, ret42, ret43, ret44);
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4D(ret11, ret12, ret13, ret14));
            m.SetRow(1, new Vector4D(ret21, ret22, ret23, ret24));
            m.SetRow(2, new Vector4D(ret31, ret32, ret33, ret34));
            m.SetRow(3, new Vector4D(ret41, ret42, ret43, ret44));
            return m;
        }

        /// <summary>
        /// Calculates the determinat of a 4x4 matrix
        /// Note that the unary ~ operator of Matrix4x4 does the same
        /// </summary>
        /// <param name="A"></param>
        /// <returns>Determinat of input matrix A</returns>
        public static float Det(Matrix4x4 A)
        {
            float a11 = A.m00, a12 = A.m01, a13 = A.m02, a14 = A.m03;
            float a21 = A.m10, a22 = A.m11, a23 = A.m12, a24 = A.m13;
            float a31 = A.m20, a32 = A.m21, a33 = A.m22, a34 = A.m23;
            float a41 = A.m30, a42 = A.m31, a43 = A.m32, a44 = A.m33;

            float term1 = a31 * a42 - a32 * a41;
            float term2 = a31 * a43 - a33 * a41;
            float term3 = a31 * a44 - a34 * a41;
            float term4 = a32 * a43 - a33 * a42;
            float term5 = a32 * a44 - a34 * a42;
            float term6 = a33 * a44 - a34 * a43;

            float subterm1 = (term6 * a22 - term5 * a23 + term4 * a24);
            float subterm2 = -(term6 * a21 - term3 * a23 + term2 * a24);
            float subterm3 = (term5 * a21 - term3 * a22 + term1 * a24);
            float subterm4 = -(term4 * a21 - term2 * a22 + term1 * a23);

            return subterm1 * a11 + subterm2 * a12 + subterm3 * a13 + subterm4 * a14;
        }

        /// <summary>
        /// Builds a matrix that interpolates 4d-vectors like a 2d bilinear interpolation in x and y direction
        /// 
        /// Should be used to transform 4d vectors with interpolation foacors in the 4d-form (x, y, x*y, 1) 
        /// </summary>
        /// <param name="P1">Upper left vector</param>
        /// <param name="P2">Upper right vector</param>
        /// <param name="P3">Lower right vector</param>
        /// <param name="P4">Lower left vector</param>
        /// <returns>Linear interpolation matrix, can be used to interpolate 4d vectors with interpolation factors in the 4d-form (x, y, x*y, 1)</returns>
        public static Matrix4x4 BilerpMatrix(Vector4D P1, Vector4D P2, Vector4D P3, Vector4D P4)
        {
            //return new Matrix4x4(P4.x - P3.x, P4.y - P3.y, P4.z - P3.z, P4.w - P3.w,
            //                     P1.x - P3.x, P1.y - P3.y, P1.z - P3.z, P1.w - P3.w,
            //                     P3.x + P2.x - P4.x - P1.x, P3.y + P2.y - P4.y - P1.y, P3.z + P2.z - P4.z - P1.z, P3.w + P2.w - P4.w - P1.w,
            //                     P3.x, P3.y, P3.z, P3.w);
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4D(P4.x - P3.x, P4.y - P3.y, P4.z - P3.z, P4.w - P3.w));
            m.SetRow(1, new Vector4D(P1.x - P3.x, P1.y - P3.y, P1.z - P3.z, P1.w - P3.w));
            m.SetRow(2, new Vector4D(P3.x + P2.x - P4.x - P1.x, P3.y + P2.y - P4.y - P1.y, P3.z + P2.z - P4.z - P1.z, P3.w + P2.w - P4.w - P1.w));
            m.SetRow(3, new Vector4D(P3.x, P3.y, P3.z, P3.w));
            return m;

        }


        #endregion transforms

    }

    #endregion VMath class

}