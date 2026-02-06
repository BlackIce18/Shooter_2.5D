using TMPro;
using UnityEngine;
using System.Collections.Generic;
public class TMP_TextEffects : MonoBehaviour
{
[Header("Wave")]
    public float waveAmplitude = 3f;
    public float waveFrequency = 2f;
    public float waveSpeed = 5f;

    [Header("Shake")]
    public float shakeStrength = 10f;
    public float shakeSpeed = 15f;

    private TMP_Text _text;

    private readonly HashSet<int> _waveChars = new();
    private readonly HashSet<int> _shakeChars = new();

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        ParseTags();
    }

    private void Update()
    {
        Animate();
    }

    public void SetText(string text)
    {
        _text.text = text;
        ParseTags();
    }

    private void ParseTags()
    {
        _waveChars.Clear();
        _shakeChars.Clear();

        string raw = _text.text;
        var clean = new System.Text.StringBuilder();

        bool wave = false;
        bool shake = false;

        int cleanIndex = 0;

        for (int i = 0; i < raw.Length; i++)
        {
            // <wave>
            if (raw.Substring(i).StartsWith("<wave>"))
            {
                wave = true;
                i += 5;
                continue;
            }

            // </wave>
            if (raw.Substring(i).StartsWith("</wave>"))
            {
                wave = false;
                i += 6;
                continue;
            }

            // <shake>
            if (raw.Substring(i).StartsWith("<shake>"))
            {
                shake = true;
                i += 6;
                continue;
            }

            // </shake>
            if (raw.Substring(i).StartsWith("</shake>"))
            {
                shake = false;
                i += 7;
                continue;
            }

            // стандартные TMP теги
            if (raw[i] == '<')
            {
                while (i < raw.Length && raw[i] != '>')
                    i++;
                continue;
            }

            if (wave)
                _waveChars.Add(cleanIndex);

            if (shake)
                _shakeChars.Add(cleanIndex);

            clean.Append(raw[i]);
            cleanIndex++;
        }

        _text.text = clean.ToString();
        _text.ForceMeshUpdate();
    }

    // -------------------- ANIMATION --------------------

    private void Animate()
    {
        _text.ForceMeshUpdate();
        var textInfo = _text.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!_waveChars.Contains(i) && !_shakeChars.Contains(i))
                continue;

            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible)
                continue;

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

            Vector3 offset = Vector3.zero;

            // WAVE
            if (_waveChars.Contains(i))
            {
                float wave =
                    Mathf.Sin(Time.time * waveSpeed + i * waveFrequency) * waveAmplitude;
                offset.y += wave;
            }

            // SHAKE
            if (_shakeChars.Contains(i))
            {
                float time = Time.time * shakeSpeed;

                Vector2 shake = new Vector2(
                    Mathf.PerlinNoise(i, time) - 0.5f,
                    Mathf.PerlinNoise(time, i) - 0.5f
                ) * shakeStrength;

                offset += (Vector3)shake;
            }

            vertices[vertexIndex + 0] += offset;
            vertices[vertexIndex + 1] += offset;
            vertices[vertexIndex + 2] += offset;
            vertices[vertexIndex + 3] += offset;
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            _text.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
