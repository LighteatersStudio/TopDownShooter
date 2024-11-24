using System;
using System.Collections.Generic;
using UnityEngine;

namespace RayFire
{
    [SelectionBase]
    [AddComponentMenu ("RayFire/Rayfire Dust")]
    [HelpURL ("https://rayfirestudios.com/unity-online-help/components/unity-dust-component/")]
    public class RayfireDust : MonoBehaviour
    {
        // UI
        public bool                    onDemolition;
        public bool                    onActivation;
        public bool                    onImpact;
        public float                   opacity;
        public Material                dustMaterial;
        public List<Material>          dustMaterials;
        public Material                emissionMaterial;
        public RFParticleEmission      emission;
        public RFParticleDynamicDust   dynamic;
        public RFParticleNoise         noise;
        public RFParticleCollisionDust collision;
        public RFParticleLimitations   limitations;
        public RFParticleRendering     rendering;
        
        // Non Serialized
        [NonSerialized] public RayfireRigid      rigid;
        [NonSerialized] public ParticleSystem    pSystem;
        [NonSerialized] public Transform         hostTm;
        [NonSerialized] public bool              initialized;
        [NonSerialized] public List<RayfireDust> children;
        [NonSerialized] public int               amountFinal;
        [NonSerialized] public bool              oldChild;
        
        // auto alpha fade
        // few dust textures with separate alphas

        /// /////////////////////////////////////////////////////////
        /// Common
        /// /////////////////////////////////////////////////////////

        // Constructor
        public RayfireDust()
        {
            onDemolition     = false;
            onActivation     = false;
            onImpact         = false;
            dustMaterial     = null;
            opacity          = 0.25f;
            emissionMaterial = null;
            emission         = new RFParticleEmission();
            dynamic          = new RFParticleDynamicDust();
            noise            = new RFParticleNoise();
            collision        = new RFParticleCollisionDust();
            limitations      = new RFParticleLimitations();
            rendering        = new RFParticleRendering();
            amountFinal      = 5;
        }

        // Copy from
        public void CopyFrom(RayfireDust source)
        {
            onDemolition     = source.onDemolition;
            onActivation     = source.onActivation;
            onImpact         = source.onImpact;
            opacity          = source.opacity;
            dustMaterial     = source.dustMaterial;
            dustMaterials    = source.dustMaterials;
            emissionMaterial = source.emissionMaterial;
            emission.CopyFrom (source.emission);
            dynamic.CopyFrom (source.dynamic);
            noise.CopyFrom (source.noise);
            collision.CopyFrom (source.collision);
            limitations.CopyFrom (source.limitations);
            rendering.CopyFrom (source.rendering);
            initialized = source.initialized;
        }

        /// /////////////////////////////////////////////////////////
        /// Methods
        /// ///////////////////////////////////////////////////////// 

        // Initialize
        public void Initialize()
        {
            // Remove null materials
            if (HasMaterials == true)
                for (int i = dustMaterials.Count - 1; i >= 0; i--)
                    if (dustMaterials[i] == null)
                        dustMaterials.RemoveAt (i);
            
            // No material
            if (dustMaterial == null && HasMaterials == false)
            {
                Debug.Log (gameObject.name + ": Dust material not defined.", gameObject);
                initialized = false;
                return;
            }
            
            initialized = true;
        }
        
        // Emit particles
        public ParticleSystem Emit()
        {
            // Initialize
            Initialize();
            
            // Emitter is not ready
            if (initialized == false)
                return null;
            
            // Particle system
            ParticleSystem ps = RFParticles.CreateParticleSystemDust(this, transform);

            // Get components
            MeshFilter emitMeshFilter = GetComponent<MeshFilter>();
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            // Get emit material index
            int emitMatIndex = RFParticles.GetEmissionMatIndex (meshRenderer, emissionMaterial);
            
            // Set amount
            amountFinal = emission.burstAmount;
            
            // Create debris
            CreateDust(this, emitMeshFilter, emitMatIndex, ps);

            return ps;
        }
        
        // Clean particle systems
        public void Clean()
        {
            // Destroy own particles
            if (hostTm != null)
                Destroy (hostTm.gameObject);

            // Destroy particles on children debris
            if (HasChildren == true)
                for (int i = 0; i < children.Count; i++)
                    if (children[i] != null)
                        if (children[i].hostTm != null)
                            Destroy (children[i].hostTm.gameObject);
        }
        
        /// /////////////////////////////////////////////////////////
        /// Create common
        /// /////////////////////////////////////////////////////////

        // Create single dust particle system
        public static void CreateDust(RayfireDust scr, MeshFilter emitMeshFilter, int emitMatIndex, ParticleSystem ps)
        {
            // Set main module
            RFParticles.SetMain(ps.main, scr.emission.lifeMin, scr.emission.lifeMax, scr.emission.sizeMin, scr.emission.sizeMax, 
                scr.dynamic.gravityMin, scr.dynamic.gravityMax, scr.dynamic.speedMin, scr.dynamic.speedMax, 
                6f, scr.limitations.maxParticles, scr.emission.duration);

            // Emission over distance
            RFParticles.SetEmission(ps.emission, scr.emission.distanceRate, (short)scr.amountFinal);
            
            // Emission from mesh or from impact point
            if (emitMeshFilter != null)
                RFParticles.SetShapeMesh(ps.shape, emitMeshFilter.sharedMesh, emitMatIndex, emitMeshFilter.transform.localScale);
            else
                RFParticles.SetShapeObject(ps.shape);
            
            // Collision
            RFParticles.SetCollisionDust(ps.collision,  scr.collision);

            // Color over life time
            RFParticles.SetColorOverLife(ps.colorOverLifetime, scr.opacity);

            // Rotation over lifetime
            RFParticles.SetRotationOverLifeTime (ps.rotationOverLifetime, scr.dynamic);
            
            // Noise
            RFParticles.SetNoise(ps.noise, scr.noise);

            // Renderer
            RFParticles.SetParticleRendererDust(ps.GetComponent<ParticleSystemRenderer>(), scr);
            
            // Start playing
            ps.Play();
        }

        public bool HasChildren { get { return children != null && children.Count > 0; } }
        public bool HasMaterials { get { return dustMaterials != null && dustMaterials.Count > 0; } }
    }
}