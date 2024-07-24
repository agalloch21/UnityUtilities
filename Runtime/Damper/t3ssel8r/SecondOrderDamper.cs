using Unity.Mathematics;
using UnityEngine;

namespace Xiaobo.UnityToolkit.Damper
{
    public interface IArithmetic<T>
    {
        T Add(T a, T b);
        T Sub(T a, T b);
        T MulFloat(T a, float b);
        T DivFloat(T a, float b);
    }
    public class SecondOrderDamper<T> : IArithmetic<T>
    {
        float f;
        public float F
        {
            get { return f; }
            set { f = value; CalculateConstrains(); }
        }
        float z;
        public float Z
        {
            get { return z; }
            set { z = value; CalculateConstrains(); }
        }
        float r;
        public float R
        {
            get { return r; }
            set { r = value; CalculateConstrains(); }
        }

        float xp; // previous input
        float y, yd; // state variables
        float _w, _z, _d, k1, k2, k3; // constrants
        T srcValue;
        T srcVel;

        T dstValue;
        T dstVel;

        public T Value
        {
            get
            {
                return dstValue;
            }
            set
            {
                srcVel = DivFloat(Sub(value, srcValue), Time.deltaTime);
                srcValue = value;

                CalculateDamperValue(Time.deltaTime);
            }
        }

        void CalculateConstrains()
        {
            // compute constants
            _w = 2 * Mathf.PI * f;
            _z = z;
            _d = _w * Mathf.Sqrt(Mathf.Abs(z * z - 1));

            k1 = z / (Mathf.PI * f);
            k2 = 1 / (_w * _w);
            k3 = r * z / _w;
        }

        void CalculateDamperValue(float T)
        {
            float k1_stable, k2_stable;
            if (_w * T < _z) // clamp k2 to guarantee stability without jitter
            {
                k1_stable = k1;
                k2_stable = Mathf.Max(k2, T * T / 2f + T * k1 / 2f, T * k1);
            }
            else // use pole matching when the system is very fast
            {
                float t1 = Mathf.Exp(-_z * _w * T);
                float alpha = 2 * t1 * (_z <= 1 ? (float)System.Math.Cos(T * _d) : (float)System.Math.Cosh(T * _d));
                float beta = t1 * t1;
                float t2 = T / (1 + beta - alpha);
                k1_stable = (1 - beta) * t2;
                k2_stable = T * t2;
            }

            dstValue = Add(dstValue, MulFloat(dstVel, T)); // integrate position by velocity
            dstVel = Add(dstVel, DivFloat(MulFloat((Sub(Sub(Add(srcValue, MulFloat(srcVel, k3)), dstValue), MulFloat(dstVel, k1))), T), k2_stable));  // integrate velocity by acceleration
        }

        public virtual T Add(T a, T b)
        {
            throw new System.NotImplementedException();
        }

        public virtual T DivFloat(T a, float b)
        {
            throw new System.NotImplementedException();
        }

        public virtual T MulFloat(T a, float b)
        {
            throw new System.NotImplementedException();
        }

        public virtual T Sub(T a, T b)
        {
            throw new System.NotImplementedException();
        }
    }

    public class SecondOrderDamper_float : SecondOrderDamper<float>
    {
        public override float Add(float a, float b) { return a + b; }
        public override float Sub(float a, float b) { return a - b; }
        public override float MulFloat(float a, float b) { return a * b; }
        public override float DivFloat(float a, float b) { return a / b; }
    }

    public class SecondOrderDamper_Vector3 : SecondOrderDamper<Vector3>
    {
        public override Vector3 Add(Vector3 a, Vector3 b) { return a + b; }
        public override Vector3 Sub(Vector3 a, Vector3 b) { return a - b; }
        public override Vector3 MulFloat(Vector3 a, float b) { return a * b; }
        public override Vector3 DivFloat(Vector3 a, float b) { return a / b; }
    }

    public class SecondOrderDamper_Quaternion : SecondOrderDamper<Quaternion>
    {
        public override Quaternion Add(Quaternion a, Quaternion b) { return new Quaternion(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w); }
        public override Quaternion Sub(Quaternion a, Quaternion b) { return new Quaternion(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w); }
        public override Quaternion MulFloat(Quaternion a, float b) { return new Quaternion(a.x * b, a.y * b, a.z * b, a.w * b); }
        public override Quaternion DivFloat(Quaternion a, float b) { return new Quaternion(a.x / b, a.y / b, a.z / b, a.w / b); }
    }
}
