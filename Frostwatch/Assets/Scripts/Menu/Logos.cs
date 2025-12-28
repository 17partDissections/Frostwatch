using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Q17pD.Frostwatch.Menu
{
    public class Logos : MonoBehaviour
    {
        [SerializeField] private List<Logo> _logos;
         [SerializeField] private Image _logosBg;
        [SerializeField] private Image _logosDarkeningPanel;
        [SerializeField] private float _darkeningTime = 1f;
        [SerializeField] private GameObject _mainObj;
        [SerializeField] private Image _mainDarkeningPanel;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(_darkeningTime);
            foreach(Logo logo in _logos)
            {
                logo._logoObj.SetActive(true);
                _logosDarkeningPanel.DOFade(0f, _darkeningTime);
                yield return new WaitForSeconds(_darkeningTime);
                yield return new WaitForSeconds(logo._time);
                _logosDarkeningPanel.DOFade(1f, _darkeningTime);
                yield return new WaitForSeconds(_darkeningTime);
                logo._logoObj.SetActive(false);
                
            }
            _mainDarkeningPanel.enabled = true;
            _mainObj.SetActive(true);
            _logosDarkeningPanel.enabled = false;
            _logosBg.enabled = false;
            yield return new WaitForSeconds(_darkeningTime);
            _mainDarkeningPanel.DOFade(0f, _darkeningTime);
            yield return new WaitForSeconds(_darkeningTime);
            _mainDarkeningPanel.enabled = false;
            gameObject.SetActive(false);
        }
    }
    [System.Serializable] public class Logo{ public GameObject _logoObj; [SerializeField] public float _time; }
}
