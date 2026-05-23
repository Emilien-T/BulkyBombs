Shader "Custom/PaintBrush"
{
    Properties
    {
        _MainTex  ("Paint Texture", 2D)     = "black" {}
        _BrushUV  ("Brush UV Position", Vector) = (0,0,0,0)
        _BrushSize("Brush Size", Float)     = 0.05
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            sampler2D _MainTex;
            float4 _BrushUV;
            float  _BrushSize;
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
            };
            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
            };
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }
            half4 frag(Varyings IN) : SV_Target
            {
                float2 uv  = IN.uv - _BrushUV.xy;
                float dist = length(uv);
                float alpha = smoothstep(_BrushSize, _BrushSize * 0.5, dist);
                half4 existing = tex2D(_MainTex, IN.uv);

                // Where brush covers, write 0. Outside brush, keep existing.
                return lerp(existing, half4(0,0,0,0), alpha);
            }
            ENDHLSL
        }
    }
}