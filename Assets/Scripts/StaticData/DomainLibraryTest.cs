using Domain;
using UnityEngine;

namespace StaticData
{
    public class DomainLibraryTest : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            var hw = new HelloWorld();
            Debug.Log(hw.Name);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
