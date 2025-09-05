Shader "Custom/TopLeftWipe"
{
    Properties
    {
        _MainTex ("Old Image", 2D) = "white" {}
        _NewTex ("New Image", 2D) = "white" {}
        _Progress ("Progress", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            sampler2D _NewTex;
            float _Progress;

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
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // Эффект замены по строкам сверху слева
                float threshold = saturate(_Progress);
                float edge = uv.x + (1.0 - uv.y); // левый верхний угол = 0, правый нижний = 2

                if (edge < threshold * 2.0)
                    return tex2D(_NewTex, uv);
                else
                    return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}
