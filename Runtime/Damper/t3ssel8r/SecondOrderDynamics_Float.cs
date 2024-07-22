using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xiaobo.UnityToolkit.Damper
{
    // Video 14:17
    public class SecondOrderDynamics_Float
    {
        float xp; // previous input
        float y, yd; // state variables
        float _w, _z, _d, k1, k2, k3; // constrants

        public SecondOrderDynamics_Float(float f, float z, float r, float x0)
        {
            // compute constants
            _w = 2 * Mathf.PI * f;
            _z = z;
            _d = _w * Mathf.Sqrt(Mathf.Abs(z * z - 1));

            k1 = z / (Mathf.PI * f);
            k2 = 1 / (_w * _w);
            k3 = r * z / _w;

            // initialize variables
            xp = x0;
            y = x0;
            yd = 0;
        }

        public float Update(float T, float x, float xd = float.MaxValue)
        {
            if (xd == float.MaxValue) // estimate velocity
            {
                xd = (x - xp) / T;
                xp = x;
            }

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

            y = y + T * yd; // integrate position by velocity
            yd = yd + T * (x + k3 * xd - y - k1 * yd) / k2_stable;  // integrate velocity by acceleration
            return y;
        }
    }
}