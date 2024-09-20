Shader "Custom/2WaveVisualizer"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveAmount ("Wave Amount", Float) = 0.5
        _Segments ("Segments", Float) = 64
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
            float _WaveAmount;
            float _Segments;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = v.uv;

                // 讓頂點隨著波動移動
                float angle = atan2(v.uv.y - 0.5, v.uv.x - 0.5);
                float distance = length(v.uv - float2(0.5, 0.5));

                // 根據波動強度調整頂點位置
                float wave = sin(angle * _Segments + _Time.y) * _WaveAmount;
                distance += wave * distance;

                // 更新頂點位置
                float2 newUV = float2(cos(angle), sin(angle)) * distance + 0.5;
                o.uv = newUV;

                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 返回主紋理顏色
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
