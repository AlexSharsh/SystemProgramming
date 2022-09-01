using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Collections;

namespace Homework2
{
    public class Jobs1 : MonoBehaviour
    {
        NativeArray<Vector3> Positions;
        NativeArray<Vector3> Velocities;
        NativeArray<Vector3> FinalPositions;
        void Start()
        {
            Positions = new NativeArray<Vector3>(new Vector3[] { new Vector3(1, 1, 1), new Vector3(2, 2, 2), new Vector3(3, 3, 3)}, Allocator.Persistent);
            Velocities = new NativeArray<Vector3>(new Vector3[] { new Vector3(1, 1, 1), new Vector3(2, 2, 2), new Vector3(3, 3, 3) }, Allocator.Persistent);
            FinalPositions = new NativeArray<Vector3>(new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) }, Allocator.Persistent);
            MyJob1 myJob1 = new MyJob1()
            {
                jPositions = Positions,
                jVelocities = Velocities,
                jFinalPositions = FinalPositions,
            };

            JobHandle jobHandle = myJob1.Schedule(FinalPositions.Length, 4);
            jobHandle.Complete();

            Debug.Log($"----------- IJobsParallelFor OUT ARRAY:------------");
            for (int i = 0; i < FinalPositions.Length; i++)
                Debug.Log($"{FinalPositions[i].x}, {FinalPositions[i].y}, {FinalPositions[i].z}");
            Debug.Log($"---------------------------------------------------");

            Positions.Dispose();
            Velocities.Dispose();
            FinalPositions.Dispose();
        }

        private void OnDestroy()
        {
            if (Positions != null)
                Positions.Dispose();

            if (Velocities != null)
                Velocities.Dispose();

            if (FinalPositions != null)
                FinalPositions.Dispose();
        }

        public struct MyJob1 : IJobParallelFor
        {
            [ReadOnly]
            public NativeArray<Vector3> jPositions;
            [ReadOnly]
            public NativeArray<Vector3> jVelocities;
            [WriteOnly]
            public NativeArray<Vector3> jFinalPositions;
            public void Execute(int index)
            {
                jFinalPositions[index] = jPositions[index] + jVelocities[index];
            }
        }
    }
}
