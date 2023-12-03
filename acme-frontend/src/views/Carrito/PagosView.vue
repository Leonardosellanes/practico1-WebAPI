<template>
    <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Cómo quieres pagar tu compra?" />
    <a-radio-group v-model:value="selectedMedio" style="width: 100%;">
        <a-space direction="vertical" style="width: 100%;" class="mt-8">
            <a-card class="mt-4">
                <a-row justify="space-between" align-items="center" style="width: 100%">
                    <a-radio :value="'Paypal'">
                        <h4> Paypal </h4>
                    </a-radio>
                    <img src="https://www.paypalobjects.com/webstatic/mktg/Logo/pp-logo-100px.png" 
                        alt="PayPal Logo">

                </a-row>
                <div class="mt-8" align-items="center" id="paypal-buttons" v-show="selectedMedio == 'Paypal'"></div>
            </a-card>

            <a-card>
                <a-row justify="space-between" style="width: 100%">
                    <a-radio :value="'Tarjeta'">
                        <h4> Tarjeta de crédito/débito </h4>
                    </a-radio>
                    <img class="img" width="80" :src="visamaster" />
                </a-row>
            </a-card>

            <a-card v-if="selectedMedio == 'Tarjeta'">
                <a-form layout="vertical" :model="formTarjeta">
                    <a-form-item label="Número de tarjeta">
                        <a-input :status="validateNro" v-model:value="formTarjeta.nro" placeholder="Número de tarjeta" />
                    </a-form-item>
                    <a-form-item label="Nombre y apellido">
                        <a-input :status="validateNom" v-model:value="formTarjeta.nombre" placeholder="Nombre y apellido" />
                    </a-form-item>
                    <a-row style="width: 100%">
                        <a-form-item label="Fecha de vencimiento" class="mr-8">
                            <a-input :status="validateFecha" v-model:value="formTarjeta.fecha"
                                placeholder="Fecha de vencimiento" />
                        </a-form-item>
                        <a-form-item label="Código de seguridad">
                            <a-input :status="validateCod" v-model:value="formTarjeta.cod"
                                placeholder="Código de seguridad" />
                        </a-form-item>
                    </a-row>

                </a-form>
                <a-row justify="justify-end">
                    <a-button block type="primary" size="large" @click="confirmar">
                        CONFIRMAR
                    </a-button>
                </a-row>
            </a-card>


        </a-space>
    </a-radio-group>
</template>
<script>
import visamaster from "/images/visamaster.jpg";
import paypalLogo from "/images/paypalLogo.png";
import { message } from "ant-design-vue";
import ApiTarjetas from "../../services/ApiTarjetas";
import CarritoController from "../../services/CarritoController";
import { useRouter } from "vue-router";
import { loadScript } from '@paypal/paypal-js';

const router = useRouter();

export default {
    data() {
        return {
            carrito: this.dataCarrito,
            selectedMedio: '',
            visamaster: visamaster,
            paypalLogo: paypalLogo,
            formTarjeta: {
                nro: '',
                nombre: '',
                fecha: '',
                cod: ''
            },
            submit: false,
            router: useRouter()
        }
    },
    mounted: function () {
        loadScript({ 'client-id': 'Ae6_5Ia8rJzNPtZL_XG0OjGNq_GuU5Zn2TjngYFwBbQVkZR-d8VIMENDZMlPIPFWeh6yAPghK4RmEWC6' }).then(() => {
            paypal.Buttons({
                createOrder: (data, actions) => {
                    return actions.order.create({
                        purchase_units: [
                            {
                                amount: {
                                    value: this.carrito.total,
                                },
                            },
                        ],
                    });
                },
                onApprove: (data, actions) => {
                    return actions.order.capture().then((details) => {
                        console.log('Compra completada:', details);
                        this.carrito.estadoOrden = 'Pendiente';
                        this.carrito.medioDePago = 'Paypal';
                        CarritoController.actualizarOrden(this.carrito).then((response) => {
                            if (response.status == 200) {
                                this.router.push('/Home')
                            }
                        })
                    });
                },
                onError: (err) => {
                    console.error('Error en el pago:', err);
                    message.error('Error al procesar el pago')
                },
            }).render('#paypal-buttons');
        });
    },

    props: {
        dataCarrito: Object
    },
    methods: {
        confirmar() {
            this.submit = true;
            if (this.selectedMedio == 'Tarjeta') {
                if (this.validateNro + this.validateNom + this.validateFecha + this.validateCod != '') {
                    return;
                }
                ApiTarjetas.validarTarjeta(this.formTarjeta.nro.toString()).then((response) => {

                    if (response.status == 200) {
                        message.success(response.data);
                        this.carrito.estadoOrden = 'Pendiente';
                        this.carrito.medioDePago = 'Tarjeta crédito/débito';
                        CarritoController.actualizarOrden(this.carrito).then((response) => {
                            if (response.status == 200) {
                                this.router.push('/Home')
                            }
                        })
                    }
                }).catch((error) => {
                    console.log(error);
                    message.error(error.response.data);
                })
            } else {

            }
        },

    },
    components: { visamaster, paypalLogo },
    computed: {
        validateNro() {
            if (this.submit && this.selectedMedio == 'Tarjeta' && this.formTarjeta.nro == '') {
                return 'error';
            } else {
                return '';
            }
        },
        validateNom() {
            if (this.submit && this.selectedMedio == 'Tarjeta' && this.formTarjeta.nombre == '') {
                return 'error';
            } else {
                return '';
            }
        },
        validateFecha() {
            if (this.submit && this.selectedMedio == 'Tarjeta' && (this.formTarjeta.fecha == '' || this.formTarjeta.fecha.length > 5)) {
                return 'error';
            } else {
                return '';
            }
        },
        validateCod() {
            if (this.submit && this.selectedMedio == 'Tarjeta' && (this.formTarjeta.cod == '' || this.formTarjeta.cod.length != 3)) {
                return 'error';
            } else {
                return '';
            }
        }
    }

}
</script>