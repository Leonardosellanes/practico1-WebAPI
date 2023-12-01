<template>
    
    <div class="my-card" ref="map"></div>


</template>
  
<script>

    export default{
        data(){
            return{
                map: null,
                address: "",
                center: { lat: -34.901113, lng: -56.164531 },
                loading : true,
                
            }
        },

        props:{
            latitud: String,
            longitud: String,
            nombre: String
        },
        methods: {
            initMap() {
                if(this.map){
                    this.map = null;
                }
                if(this.latitud != '' && this.longitud != ''){
                    this.center.lat = this.latitud;
                    this.center.lng = this.longitud;
                }
                this.map = new window.google.maps.Map(this.$refs.map, {
                    center: this.center,
                    zoom: 10,
                });
                const marker = new google.maps.Marker({
                    position: this.center,
                    label: this.nombre,
                    map: this.map,
                });
                this.marker = marker;

                window.google.maps.event.addListener(this.map, "click", (event) => {
                    this.addMarker(event.latLng, this.map);
                });
            },
            addMarker(location, map) {
                if (this.marker) {
                    this.marker.setMap(null);
                    this.marker = null;
                }
                const marker = new google.maps.Marker({
                    position: location,
                    label: this.nombre,
                    map: map,
                });
                console.log(marker);
                this.marker = marker;
                this.$emit('actualizar-coordenadas', { latitud: location.lat(), longitud: location.lng() });
            },
    
        },

        watch: {
            
            longitud() {
                this.initMap();
            },
            latitud(){
                this.initMap();
            }
        },

    }
</script>

<style scoped>
    .my-card {
        border-radius: 24px !important;
        height: 600px;
    }

</style>