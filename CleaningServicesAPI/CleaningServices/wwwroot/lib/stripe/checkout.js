// This is your test publishable API key.
const stripe = Stripe("pk_test_51OEY5FEU0SXAQ8qqCOpaEKtv0yMO5ziRcAeLEndfhVtK8tiTFYPkh8XvZFmUWR9AFT7BVcG7twpO5ypUR5PcnEt500JqMOQfTE");



// Create a Checkout Session as soon as the page loads
async function initialize(priceid) {
    $("#btnHome").show();
  const response = await fetch("https://localhost:44317/CheckoutApi?prodid="+priceid , {
    method: "POST",
	
    
  });

  const { clientSecret } = await response.json();

  const checkout = await stripe.initEmbeddedCheckout({
    clientSecret,
  });

  // Mount Checkout
  checkout.mount('#checkout');
}