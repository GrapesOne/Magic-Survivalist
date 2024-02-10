using System.Collections;
using UnityEngine;

namespace Code.Core.Utilities {

    public interface ICoroutineRunner {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }

}
