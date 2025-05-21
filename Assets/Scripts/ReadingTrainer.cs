using System.Collections;
using UnityEngine;

public class ReadingTrainer : MonoBehaviour
{
    public void ClearRotations()
    {
        StartCoroutine(ResetRotations());
    }
    private IEnumerator ResetRotations()
    {
        yield return new WaitForSeconds(1f);
        // transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
