Shader "Custom/VolumetricBlood_Final"
{
    Properties
    {
        _NoiseTex("Noise Texture", 2D) = "gray" {}
        [NoScaleOffset] _NoiseTex_ST("Noise Tiling and Offset", Vector) = (1.6, 1, 0.59, 0)
        _BloodColor("Blood Color", Color) = (1,0,0,1)
        _SliceAmount("Slice Amount", Range(0,1)) = 0.5
        _VertexDisplacement("Vertex Displacement", Float) = 1.0
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent" "RenderType"="Transparent"
        }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma target 3.5
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;
            float4 _BloodColor;
            float _SliceAmount;
            float _VertexDisplacement;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

             struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float noiseVal : TEXCOORD1;
                float objectY : TEXCOORD2;
            };

            
            float SampleTriplanarNoise(float3 worldPos)
            {
                float3 blend = abs(normalize(worldPos));
                blend = blend / (blend.x + blend.y + blend.z + 1e-5);
                
                float2 uvXZ = worldPos.xz * _NoiseTex_ST.x + _NoiseTex_ST.z;
                float2 uvXY = worldPos.xy * _NoiseTex_ST.x + _NoiseTex_ST.z;
                float2 uvYZ = worldPos.yz * _NoiseTex_ST.x + _NoiseTex_ST.z;

                float x = tex2Dlod(_NoiseTex, float4(uvYZ, 0, 0)).r;
                float y = tex2Dlod(_NoiseTex, float4(uvXZ, 0, 0)).r;
                float z = tex2Dlod(_NoiseTex, float4(uvXY, 0, 0)).r;

                return x * blend.x + y * blend.y + z * blend.z;
            }

             v2f vert(appdata v)
            {
                v2f o;
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                float3 viewDir = normalize(_WorldSpaceCameraPos - worldPos);
                
                float noise = SampleTriplanarNoise(worldPos);
                float facing = saturate(dot(worldNormal, viewDir));
                float3 displaced = v.vertex.xyz + v.normal * _VertexDisplacement * noise * facing;

                o.vertex = UnityObjectToClipPos(float4(displaced, 1.0));
                o.worldPos = worldPos;
                o.noiseVal = noise;
                
                o.objectY = v.vertex.y;
                
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float minY = -2;  
                float maxY = 2;
                float normalizedY = (i.objectY - minY) / (maxY - minY);
                
                normalizedY = 1.0 - normalizedY;
                
                float cutoff = _SliceAmount * 2.0 - (1.0 - normalizedY);
                
                if (i.noiseVal < cutoff)
                    discard;
                
                return _BloodColor;
            }
            ENDCG
        }
    }
}