    Shader "Hidden/glareFxCheapMobile" {
    Properties {
        _MainTex ("Input", 2D) = "white" {}
        _OrgTex ("Input", 2D) = "white" {}
        _lensDirt ("Input", 2D) = "white" {}
        
        _threshold ("", float) = 0.5
        _int ("", float) = 1.0
    }
        SubShader {
            Pass {
	  ZTest Off Cull Off ZWrite Off Blend Off
	  Fog { Mode off }              
           
        Program "vp" {
// Vertex combos: 1
//   opengl - ALU: 5 to 5
//   d3d9 - ALU: 13 to 13
SubProgram "opengl " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
"!!ARBvp1.0
# 5 ALU
PARAM c[5] = { program.local[0],
		state.matrix.mvp };
MOV result.texcoord[0].xy, vertex.texcoord[0];
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 5 instructions, 0 R-regs
"
}

SubProgram "d3d9 " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Vector 4 [_OrgTex_TexelSize]
"vs_2_0
; 13 ALU
def c5, 0.00000000, 1.00000000, 0, 0
dcl_position0 v0
dcl_texcoord0 v1
mov r0.x, c5
slt r0.x, c4.y, r0
max r0.x, -r0, r0
slt r0.x, c5, r0
add r0.y, -r0.x, c5
mul r0.z, v1.y, r0.y
add r0.y, -v1, c5
mad oT0.y, r0.x, r0, r0.z
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
mov oT0.x, v1
"
}

SubProgram "gles " {
Keywords { }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying mediump vec2 xlv_TEXCOORD0;

attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  mediump vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  tmpvar_2 = (gl_ModelViewProjectionMatrix * _glesVertex);
  tmpvar_1 = tmpvar_2;
  gl_Position = tmpvar_1;
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
}



#endif
#ifdef FRAGMENT

varying mediump vec2 xlv_TEXCOORD0;
uniform mediump float _threshold;
uniform sampler2D _lensDirt;
uniform mediump float _int;
uniform sampler2D _OrgTex;
uniform sampler2D _MainTex;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = texture2D (_OrgTex, xlv_TEXCOORD0);
  mediump vec2 tmpvar_2;
  tmpvar_2 = (-(xlv_TEXCOORD0) + 1.0);
  mediump vec2 tmpvar_3;
  tmpvar_3 = ((vec2(0.5, 0.5) - tmpvar_2) * 0.2);
  mediump vec2 tmpvar_4;
  tmpvar_4 = ((vec2(0.5, 0.5) - xlv_TEXCOORD0) * 0.2);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_2);
  lowp vec3 c;
  mediump vec3 tmpvar_6;
  tmpvar_6 = (tmpvar_5.xyz - vec3(_threshold));
  c = tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, (xlv_TEXCOORD0 + tmpvar_4));
  lowp vec3 c_i0;
  mediump vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7.xyz - vec3(_threshold));
  c_i0 = tmpvar_8;
  mediump vec2 tmpvar_9;
  tmpvar_9 = (tmpvar_3 * 2.0);
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture2D (_MainTex, (tmpvar_2 + tmpvar_9));
  lowp vec3 c_i0_i1;
  mediump vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10.xyz - vec3(_threshold));
  c_i0_i1 = tmpvar_11;
  mediump vec2 tmpvar_12;
  tmpvar_12 = (tmpvar_4 * 3.0);
  lowp vec4 tmpvar_13;
  tmpvar_13 = texture2D (_MainTex, (xlv_TEXCOORD0 + tmpvar_12));
  lowp vec3 c_i0_i1_i2;
  mediump vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13.xyz - vec3(_threshold));
  c_i0_i1_i2 = tmpvar_14;
  mediump vec2 tmpvar_15;
  tmpvar_15 = (tmpvar_3 * 4.0);
  lowp vec4 tmpvar_16;
  tmpvar_16 = texture2D (_MainTex, (tmpvar_2 + tmpvar_15));
  lowp vec3 c_i0_i1_i2_i3;
  mediump vec3 tmpvar_17;
  tmpvar_17 = (tmpvar_16.xyz - vec3(_threshold));
  c_i0_i1_i2_i3 = tmpvar_17;
  lowp vec4 tmpvar_18;
  tmpvar_18.w = 1.0;
  tmpvar_18.xyz = (((((clamp (c, 0.0, 1.0) + clamp (c_i0, 0.0, 1.0)) + clamp (c_i0_i1, 0.0, 1.0)) + clamp (c_i0_i1_i2, 0.0, 1.0)) + clamp (c_i0_i1_i2_i3, 0.0, 1.0)) * 0.2);
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_lensDirt, xlv_TEXCOORD0);
  gl_FragData[0] = (tmpvar_1 + ((tmpvar_18 * tmpvar_19) * _int));
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying mediump vec2 xlv_TEXCOORD0;

attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  mediump vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  tmpvar_2 = (gl_ModelViewProjectionMatrix * _glesVertex);
  tmpvar_1 = tmpvar_2;
  gl_Position = tmpvar_1;
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
}



#endif
#ifdef FRAGMENT

varying mediump vec2 xlv_TEXCOORD0;
uniform mediump float _threshold;
uniform sampler2D _lensDirt;
uniform mediump float _int;
uniform sampler2D _OrgTex;
uniform sampler2D _MainTex;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = texture2D (_OrgTex, xlv_TEXCOORD0);
  mediump vec2 tmpvar_2;
  tmpvar_2 = (-(xlv_TEXCOORD0) + 1.0);
  mediump vec2 tmpvar_3;
  tmpvar_3 = ((vec2(0.5, 0.5) - tmpvar_2) * 0.2);
  mediump vec2 tmpvar_4;
  tmpvar_4 = ((vec2(0.5, 0.5) - xlv_TEXCOORD0) * 0.2);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_2);
  lowp vec3 c;
  mediump vec3 tmpvar_6;
  tmpvar_6 = (tmpvar_5.xyz - vec3(_threshold));
  c = tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, (xlv_TEXCOORD0 + tmpvar_4));
  lowp vec3 c_i0;
  mediump vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7.xyz - vec3(_threshold));
  c_i0 = tmpvar_8;
  mediump vec2 tmpvar_9;
  tmpvar_9 = (tmpvar_3 * 2.0);
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture2D (_MainTex, (tmpvar_2 + tmpvar_9));
  lowp vec3 c_i0_i1;
  mediump vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10.xyz - vec3(_threshold));
  c_i0_i1 = tmpvar_11;
  mediump vec2 tmpvar_12;
  tmpvar_12 = (tmpvar_4 * 3.0);
  lowp vec4 tmpvar_13;
  tmpvar_13 = texture2D (_MainTex, (xlv_TEXCOORD0 + tmpvar_12));
  lowp vec3 c_i0_i1_i2;
  mediump vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13.xyz - vec3(_threshold));
  c_i0_i1_i2 = tmpvar_14;
  mediump vec2 tmpvar_15;
  tmpvar_15 = (tmpvar_3 * 4.0);
  lowp vec4 tmpvar_16;
  tmpvar_16 = texture2D (_MainTex, (tmpvar_2 + tmpvar_15));
  lowp vec3 c_i0_i1_i2_i3;
  mediump vec3 tmpvar_17;
  tmpvar_17 = (tmpvar_16.xyz - vec3(_threshold));
  c_i0_i1_i2_i3 = tmpvar_17;
  lowp vec4 tmpvar_18;
  tmpvar_18.w = 1.0;
  tmpvar_18.xyz = (((((clamp (c, 0.0, 1.0) + clamp (c_i0, 0.0, 1.0)) + clamp (c_i0_i1, 0.0, 1.0)) + clamp (c_i0_i1_i2, 0.0, 1.0)) + clamp (c_i0_i1_i2_i3, 0.0, 1.0)) * 0.2);
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_lensDirt, xlv_TEXCOORD0);
  gl_FragData[0] = (tmpvar_1 + ((tmpvar_18 * tmpvar_19) * _int));
}



#endif"
}

SubProgram "flash " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
"agal_vs
[bc]
aaaaaaaaaaaaadaeadaaaaoeaaaaaaaaaaaaaaaaaaaaaaaa mov v0.xy, a3
bdaaaaaaaaaaaiadaaaaaaoeaaaaaaaaadaaaaoeabaaaaaa dp4 o0.w, a0, c3
bdaaaaaaaaaaaeadaaaaaaoeaaaaaaaaacaaaaoeabaaaaaa dp4 o0.z, a0, c2
bdaaaaaaaaaaacadaaaaaaoeaaaaaaaaabaaaaoeabaaaaaa dp4 o0.y, a0, c1
bdaaaaaaaaaaabadaaaaaaoeaaaaaaaaaaaaaaoeabaaaaaa dp4 o0.x, a0, c0
aaaaaaaaaaaaamaeaaaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov v0.zw, c0
"
}

}
Program "fp" {
// Fragment combos: 1
//   opengl - ALU: 29 to 29, TEX: 7 to 7
//   d3d9 - ALU: 23 to 23, TEX: 7 to 7
SubProgram "opengl " {
Keywords { }
Float 0 [_threshold]
Float 1 [_int]
SetTexture 0 [_OrgTex] 2D
SetTexture 1 [_MainTex] 2D
SetTexture 2 [_lensDirt] 2D
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 29 ALU, 7 TEX
PARAM c[4] = { program.local[0..1],
		{ 1, 0.5, 0.2, 2 },
		{ 3, 4, 0.19995117 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEMP R6;
TEX R1, fragment.texcoord[0], texture[2], 2D;
ADD R2.xy, -fragment.texcoord[0], c[2].x;
ADD R0.xy, -R2, c[2].y;
MUL R0.zw, R0.xyxy, c[2].z;
MAD R4.xy, R0.zwzw, c[3].y, R2;
ADD R0.xy, -fragment.texcoord[0], c[2].y;
MUL R0.xy, R0, c[2].z;
MAD R2.zw, R0.xyxy, c[3].x, fragment.texcoord[0].xyxy;
MAD R3.zw, R0, c[2].w, R2.xyxy;
ADD R3.xy, fragment.texcoord[0], R0;
TEX R5.xyz, R2.zwzw, texture[1], 2D;
TEX R6.xyz, R4, texture[1], 2D;
TEX R4.xyz, R3.zwzw, texture[1], 2D;
TEX R2.xyz, R2, texture[1], 2D;
TEX R3.xyz, R3, texture[1], 2D;
TEX R0, fragment.texcoord[0], texture[0], 2D;
ADD_SAT R4.xyz, R4, -c[0].x;
ADD_SAT R3.xyz, R3, -c[0].x;
ADD_SAT R2.xyz, R2, -c[0].x;
ADD R2.xyz, R2, R3;
ADD R2.xyz, R2, R4;
ADD_SAT R3.xyz, R5, -c[0].x;
ADD_SAT R4.xyz, R6, -c[0].x;
ADD R2.xyz, R2, R3;
ADD R2.xyz, R2, R4;
MUL R2.xyz, R2, c[3].z;
MOV R2.w, c[2].x;
MUL R1, R2, R1;
MAD result.color, R1, c[1].x, R0;
END
# 29 instructions, 7 R-regs
"
}

SubProgram "d3d9 " {
Keywords { }
Float 0 [_threshold]
Float 1 [_int]
SetTexture 0 [_OrgTex] 2D
SetTexture 1 [_MainTex] 2D
SetTexture 2 [_lensDirt] 2D
"ps_2_0
; 23 ALU, 7 TEX
dcl_2d s0
dcl_2d s1
dcl_2d s2
def c2, 1.00000000, 0.50000000, 0.20000000, 4.00000000
def c3, 2.00000000, 3.00000000, 0.19995117, 0
dcl t0.xy
texld r2, t0, s0
add_pp r6.xy, -t0, c2.x
add_pp r0.xy, -r6, c2.y
mul r1.xy, r0, c2.z
mad_pp r3.xy, r1, c3.x, r6
mad_pp r5.xy, r1, c2.w, r6
add_pp r0.xy, -t0, c2.y
mul r0.xy, r0, c2.z
add_pp r4.xy, t0, r0
mad_pp r1.xy, r0, c3.y, t0
texld r1, r1, s1
texld r0, t0, s2
texld r5, r5, s1
texld r6, r6, s1
texld r3, r3, s1
texld r4, r4, s1
add_pp_sat r3.xyz, r3, -c0.x
add_pp_sat r4.xyz, r4, -c0.x
add_pp_sat r6.xyz, r6, -c0.x
add_pp r4.xyz, r6, r4
add_pp r3.xyz, r4, r3
add_pp_sat r1.xyz, r1, -c0.x
add_pp_sat r4.xyz, r5, -c0.x
add_pp r1.xyz, r3, r1
add_pp r1.xyz, r1, r4
mul_pp r1.xyz, r1, c3.z
mov_pp r1.w, c2.x
mul r0, r1, r0
mad r0, r0, c1.x, r2
mov_pp oC0, r0
"
}

SubProgram "gles " {
Keywords { }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { }
"!!GLES"
}

SubProgram "flash " {
Keywords { }
Float 0 [_threshold]
Float 1 [_int]
SetTexture 0 [_OrgTex] 2D
SetTexture 1 [_MainTex] 2D
SetTexture 2 [_lensDirt] 2D
"agal_ps
c2 1.0 0.5 0.2 4.0
c3 2.0 3.0 0.199951 0.0
[bc]
ciaaaaaaacaaapacaaaaaaoeaeaaaaaaaaaaaaaaafaababb tex r2, v0, s0 <2d wrap linear point>
bfaaaaaaagaaadacaaaaaaoeaeaaaaaaaaaaaaaaaaaaaaaa neg r6.xy, v0
abaaaaaaagaaadacagaaaafeacaaaaaaacaaaaaaabaaaaaa add r6.xy, r6.xyyy, c2.x
bfaaaaaaaaaaadacagaaaafeacaaaaaaaaaaaaaaaaaaaaaa neg r0.xy, r6.xyyy
abaaaaaaaaaaadacaaaaaafeacaaaaaaacaaaaffabaaaaaa add r0.xy, r0.xyyy, c2.y
adaaaaaaabaaadacaaaaaafeacaaaaaaacaaaakkabaaaaaa mul r1.xy, r0.xyyy, c2.z
adaaaaaaadaaadacabaaaafeacaaaaaaadaaaaaaabaaaaaa mul r3.xy, r1.xyyy, c3.x
abaaaaaaadaaadacadaaaafeacaaaaaaagaaaafeacaaaaaa add r3.xy, r3.xyyy, r6.xyyy
adaaaaaaafaaadacabaaaafeacaaaaaaacaaaappabaaaaaa mul r5.xy, r1.xyyy, c2.w
abaaaaaaafaaadacafaaaafeacaaaaaaagaaaafeacaaaaaa add r5.xy, r5.xyyy, r6.xyyy
bfaaaaaaaaaaadacaaaaaaoeaeaaaaaaaaaaaaaaaaaaaaaa neg r0.xy, v0
abaaaaaaaaaaadacaaaaaafeacaaaaaaacaaaaffabaaaaaa add r0.xy, r0.xyyy, c2.y
adaaaaaaaaaaadacaaaaaafeacaaaaaaacaaaakkabaaaaaa mul r0.xy, r0.xyyy, c2.z
abaaaaaaaeaaadacaaaaaaoeaeaaaaaaaaaaaafeacaaaaaa add r4.xy, v0, r0.xyyy
adaaaaaaabaaadacaaaaaafeacaaaaaaadaaaaffabaaaaaa mul r1.xy, r0.xyyy, c3.y
abaaaaaaabaaadacabaaaafeacaaaaaaaaaaaaoeaeaaaaaa add r1.xy, r1.xyyy, v0
ciaaaaaaabaaapacabaaaafeacaaaaaaabaaaaaaafaababb tex r1, r1.xyyy, s1 <2d wrap linear point>
ciaaaaaaaaaaapacaaaaaaoeaeaaaaaaacaaaaaaafaababb tex r0, v0, s2 <2d wrap linear point>
ciaaaaaaafaaapacafaaaafeacaaaaaaabaaaaaaafaababb tex r5, r5.xyyy, s1 <2d wrap linear point>
ciaaaaaaagaaapacagaaaafeacaaaaaaabaaaaaaafaababb tex r6, r6.xyyy, s1 <2d wrap linear point>
ciaaaaaaadaaapacadaaaafeacaaaaaaabaaaaaaafaababb tex r3, r3.xyyy, s1 <2d wrap linear point>
ciaaaaaaaeaaapacaeaaaafeacaaaaaaabaaaaaaafaababb tex r4, r4.xyyy, s1 <2d wrap linear point>
acaaaaaaadaaahacadaaaakeacaaaaaaaaaaaaaaabaaaaaa sub r3.xyz, r3.xyzz, c0.x
bgaaaaaaadaaahacadaaaakeacaaaaaaaaaaaaaaaaaaaaaa sat r3.xyz, r3.xyzz
acaaaaaaaeaaahacaeaaaakeacaaaaaaaaaaaaaaabaaaaaa sub r4.xyz, r4.xyzz, c0.x
bgaaaaaaaeaaahacaeaaaakeacaaaaaaaaaaaaaaaaaaaaaa sat r4.xyz, r4.xyzz
acaaaaaaagaaahacagaaaakeacaaaaaaaaaaaaaaabaaaaaa sub r6.xyz, r6.xyzz, c0.x
bgaaaaaaagaaahacagaaaakeacaaaaaaaaaaaaaaaaaaaaaa sat r6.xyz, r6.xyzz
abaaaaaaaeaaahacagaaaakeacaaaaaaaeaaaakeacaaaaaa add r4.xyz, r6.xyzz, r4.xyzz
abaaaaaaadaaahacaeaaaakeacaaaaaaadaaaakeacaaaaaa add r3.xyz, r4.xyzz, r3.xyzz
acaaaaaaabaaahacabaaaakeacaaaaaaaaaaaaaaabaaaaaa sub r1.xyz, r1.xyzz, c0.x
bgaaaaaaabaaahacabaaaakeacaaaaaaaaaaaaaaaaaaaaaa sat r1.xyz, r1.xyzz
acaaaaaaaeaaahacafaaaakeacaaaaaaaaaaaaaaabaaaaaa sub r4.xyz, r5.xyzz, c0.x
bgaaaaaaaeaaahacaeaaaakeacaaaaaaaaaaaaaaaaaaaaaa sat r4.xyz, r4.xyzz
abaaaaaaabaaahacadaaaakeacaaaaaaabaaaakeacaaaaaa add r1.xyz, r3.xyzz, r1.xyzz
abaaaaaaabaaahacabaaaakeacaaaaaaaeaaaakeacaaaaaa add r1.xyz, r1.xyzz, r4.xyzz
adaaaaaaabaaahacabaaaakeacaaaaaaadaaaakkabaaaaaa mul r1.xyz, r1.xyzz, c3.z
aaaaaaaaabaaaiacacaaaaaaabaaaaaaaaaaaaaaaaaaaaaa mov r1.w, c2.x
adaaaaaaaaaaapacabaaaaoeacaaaaaaaaaaaaoeacaaaaaa mul r0, r1, r0
adaaaaaaaaaaapacaaaaaaoeacaaaaaaabaaaaaaabaaaaaa mul r0, r0, c1.x
abaaaaaaaaaaapacaaaaaaoeacaaaaaaacaaaaoeacaaaaaa add r0, r0, r2
aaaaaaaaaaaaapadaaaaaaoeacaaaaaaaaaaaaaaaaaaaaaa mov o0, r0
"
}

}

#LINE 111

            }
        }
    }