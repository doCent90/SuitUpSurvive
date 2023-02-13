using System.Collections;
using UnityEngine;

namespace Assets.Source
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
