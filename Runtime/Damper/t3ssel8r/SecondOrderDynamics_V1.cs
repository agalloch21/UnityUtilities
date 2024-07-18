using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xiaobo.UnityToolkit.Damper
{
    // Video 13:21
    public class SecondOrderDynamics_V1
    {
        Vector3 xp; // previous input
        Vector3 y, yd; // state variables
        float k1, k2, k3; // dynamics constrants

        public SecondOrderDynamics_V1(float f, float z, float r, Vector3 x0)
        {
            // compute constants
            k1 = z / (Mathf.PI * f);
            k2 = 1 / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
            k3 = r * z / (2f * Mathf.PI * f);

            // initialize variables
            xp = x0;
            y = x0;
            yd = Vector3.zero;
        }

        public Vector3 Update(float T, Vector3 x, Vector3 xd)
        {
            if (xd == Vector3.one * float.MaxValue) // estimate velocity
            {
                xd = (x - xp) / T;
                xp = x;
            }

            float k2_stable = Mathf.Max(k2, 1.1f * (T * T / 4f + T * k1 / 2f)); // clamp k2 to guarantee stability without jitter
            y = y + T * yd; // integrate position by velocity
            yd = yd + T * (x + k3 * xd - y - k1 * yd) / k2_stable;  // integrate velocity by acceleration
            return y;
        }
    }
}