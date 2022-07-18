using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Animations;

namespace Tests
{
    public static class TestUtil
    {
        public static IEnumerable<Transform> EnumChildrenRecursive(this Transform parent)
        {
            return parent
                .GetComponentsInChildren<Transform>() // 親を含む子を再帰的に取得
                .Skip(1); // 親をスキップする
        }
        public static int GetConstraintsCount(GameObject sourceObject)
        {
            int constraints_count = 0;
            void setConstraint<T>(T[] constraints) where T : Behaviour, IConstraint
            {
                foreach (T constraint in constraints)
                {
                    constraints_count++;
                }
            }

            bool includeInactive = true;
            setConstraint(sourceObject.GetComponentsInChildren<AimConstraint>(includeInactive));
            setConstraint(sourceObject.GetComponentsInChildren<LookAtConstraint>(includeInactive));
            setConstraint(sourceObject.GetComponentsInChildren<ParentConstraint>(includeInactive));
            setConstraint(sourceObject.GetComponentsInChildren<PositionConstraint>(includeInactive));
            setConstraint(sourceObject.GetComponentsInChildren<RotationConstraint>(includeInactive));
            setConstraint(sourceObject.GetComponentsInChildren<ScaleConstraint>(includeInactive));

            return constraints_count;
        }
    }
}
