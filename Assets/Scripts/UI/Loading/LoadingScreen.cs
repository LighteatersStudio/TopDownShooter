using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Services.AppVersion.Loading;
using TMPro;
using UI.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadingScreen : View
    {
        private const float FillDelay = 0.15f;
        
        [SerializeField]
        private Slider _progressFill;
        [SerializeField]
        private TextMeshProUGUI _loadingInfo;
        [SerializeField]
        private float _barSpeed = 1;
        
        private float _targetProgress;
        private bool _isProgress;
        
        public async UniTask Load(Queue<ILoadingOperation> loadingOperations, bool closeAfterLoad = true)
        {
            _isProgress = true;
            
            StartCoroutine(UpdateProgressBar());
            
            foreach (var operation in loadingOperations)
            {
                ResetFill();
                _loadingInfo.text = operation.Description;

                await operation.Launch(OnProgress);
                await WaitForBarFill();
            }

            _isProgress = false;
            
            if (closeAfterLoad)
            {
                Close();
            }
        }

        private void ResetFill()
        {
            _progressFill.value = 0;
            _targetProgress = 0;
        }

        private void OnProgress(float progress)
        {
            _targetProgress = progress;
        }

        private async UniTask WaitForBarFill()
        {
            while (_progressFill.value < _targetProgress)
            {
                await UniTask.Delay(1);
            }
            
            await UniTask.Delay(TimeSpan.FromSeconds(FillDelay));
        }

        private IEnumerator UpdateProgressBar()
        {
            while (_isProgress)
            {
                if (_progressFill.value < _targetProgress)
                {
                    _progressFill.value += Time.deltaTime * _barSpeed;
                }
                
                yield return null;    
            }
        }
    }
}