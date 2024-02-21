using UnityEngine;

namespace GameLogic.Extensions
{
    public static class PhysicsExtensions
    {

        public static void ApplyForceToReachVelocity(this Rigidbody rigidbody, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
        {
            if (force == 0 || velocity.magnitude == 0) 
                return;

            velocity += velocity.normalized * 0.2f * rigidbody.drag;
            force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

            if (rigidbody.velocity.magnitude == 0)
            {
                rigidbody.AddForce(velocity * force, mode);
            }
            else
            {
                var velocityProjectedToTarget = velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude;
                rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
            }
        }
        

        public static Vector3 GetNormal(this Vector3[] points)
        {
            if (points.Length < 3) return Vector3.up;

            var center = points.GetCenter();
            float xx = 0f, xy = 0f, xz = 0f, yy = 0f, yz = 0f, zz = 0f;

            foreach (var point in points)
            {
                var r = point - center;
                xx += r.x * r.x;
                xy += r.x * r.y;
                xz += r.x * r.z;
                yy += r.y * r.y;
                yz += r.y * r.z;
                zz += r.z * r.z;
            }

            var det_x = yy * zz - yz * yz;
            var det_y = xx * zz - xz * xz;
            var det_z = xx * yy - xy * xy;

            if (det_x > det_y && det_x > det_z)
                return new Vector3(det_x, xz * yz - xy * zz, xy * yz - xz * yy).normalized;
            if (det_y > det_z)
                return new Vector3(xz * yz - xy * zz, det_y, xy * xz - yz * xx).normalized;
            else
                return new Vector3(xy * yz - xz * yy, xy * xz - yz * xx, det_z).normalized;
        }

        public static Vector3 GetCenter(this Vector3[] points)
        {
            var center = Vector3.zero;
            foreach (var point in points)
                center += point;
            return center / points.Length;
        }
    }
}
