Shader "Custom/1WaveVisualizer"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Frequency("Frequency", Float) = 5.0
        _Amplitude("Amplitude", Float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Frequency;
            float _Amplitude;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                // 在 xy 平面上進行波動
                float angle = atan2(v.vertex.y, v.vertex.x);
                float distance = length(v.vertex.xy);
                float wave = sin(angle * _Frequency + _Time.y) * _Amplitude;
                v.vertex.xy *= (1.0 + wave);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
